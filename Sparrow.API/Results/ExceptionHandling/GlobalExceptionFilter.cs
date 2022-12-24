using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sparrow.Core;
using Sparrow.API.Exceptions;
using Sparrow.API.Results.Models;
using System.Net;
using System.Text;

namespace Sparrow.API.Results.ExceptionHandling;

public class GlobalExceptionFilter : IExceptionFilter
{
    protected readonly ILogger _logger;
    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        SeverityAwareLog(context.Exception);

        HandleAndWrapException(context);
    }

    protected virtual void HandleAndWrapException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = GetStatusCode(context);

        bool unathorized = context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized;

        context.Result = new ObjectResult(
            new SparrowApiResponse(CreateErrorInfo(context.Exception), unathorized));

        context.Exception = null; // Handled!
        context.ExceptionHandled = true;
    }

    private ErrorInfo CreateErrorInfo(Exception exception)
    {
        var detailBuilder = new StringBuilder();

        CreateDetailsFromException(exception, detailBuilder);

        // Exlucde TechnicalMessage if it's Production env
        return new ErrorInfo("InternalServerError", detailBuilder.ToString());
    }

    private void CreateDetailsFromException(Exception exception, StringBuilder detailBuilder)
    {
        //Exception Message
        detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);

        //Exception StackTrace
        if (!string.IsNullOrEmpty(exception.StackTrace))
        {
            detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
        }

        //Inner exception
        if (exception.InnerException != null)
        {
            CreateDetailsFromException(exception.InnerException, detailBuilder);
        }
    }

    protected virtual int GetStatusCode(ExceptionContext context)
    {
        //Implement custom exceptions
        //if (context.Exception is EntityNotFoundException)
        //{
        //    return (int)HttpStatusCode.NotFound;
        //}

        // کد خطا، نبود؟
        return (int)HttpStatusCode.InternalServerError;
    }

    protected virtual void SeverityAwareLog(Exception exception)
    {
        var logSeverity = LogSeverity.Error;
        string expMessage = exception.Message;
        string expTechMessage = "";
        if (exception is SparrowException exp)
        {
            logSeverity = exp.Severity;
            expTechMessage = exp.TechnicalMessage;
        }
        // TODO: Move it to NLog configuration file log parameters
        string message = string.Format(
            "Processed an unhandled exception of type {0}:\r\nMessage: {1}\r\nTechnicalMessage: {2}",
            exception.GetType().Name, EscapeForStringFormat(expMessage), EscapeForStringFormat(expTechMessage));
        EventId eventId = exception is SparrowException mtrException ? new EventId(mtrException.ErrorCode ?? 0) : default;

        switch (logSeverity)
        {
            case LogSeverity.Debug:
                _logger.LogDebug(eventId, exception, message);
                break;
            case LogSeverity.Info:
                _logger.LogInformation(eventId, exception, message);
                break;
            case LogSeverity.Warn:
                _logger.LogWarning(eventId, exception, message);
                break;
            case LogSeverity.Error:
                _logger.LogError(exception, message, exception);
                break;
            case LogSeverity.Critical:
                _logger.LogCritical(eventId, exception, message);
                break;
            default:
                _logger.LogWarning(
                    "Invalid parameter passed to SeverityAwareLog method, Please check the code and correct the issue");
                _logger.LogError(eventId, exception, message);
                break;
        }
    }

    protected virtual string EscapeForStringFormat(string input)
    {
        StringBuilder sb = new(input);
        sb.Replace("{", "{{");
        sb.Replace("}", "}}");
        return sb.ToString();
    }
}
