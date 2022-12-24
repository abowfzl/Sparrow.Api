using Sparrow.Core;

namespace Sparrow.API.Exceptions;

public class SparrowException : Exception
{
    public SparrowException(string message, string technicalMessage = "", int? errorCode = null)
    : base(message)
    {
        ErrorCode = errorCode;
        TechnicalMessage = technicalMessage;
        Severity = LogSeverity.Error;
    }

    public SparrowException(string message, string technicalMessage, Exception innerException, int? errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        TechnicalMessage = technicalMessage;
        Severity = LogSeverity.Error;
    }

    public int? ErrorCode { get; protected set; }

    /// <summary>
    /// Technical-details are not allowed to be shown to the user.
    /// Just log them or use them internally by software-technicians.
    /// </summary>
    public string TechnicalMessage { get; protected set; }

    /// <summary>
    /// Severity of the exception. The main usage will be for logs and monitoring.
    /// This way we can distinguish various exceptions in logs, 
    /// Think about the difference of between severity of a ValidationException and an Exception related to DB connection or Infrastructure. 
    /// Default: Error.
    /// </summary>
    public LogSeverity Severity { get; protected set; }
}
