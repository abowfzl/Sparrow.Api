namespace Sparrow.API.Results.Models;

[Serializable]
public class ErrorInfo
{
    public int ErrorCode { get; set; }

    public string? Message { get; set; }

    public string? TechnicalMessage { get; set; }

    public ErrorInfo(string? message, string? technicalMessage)
    {
        Message = message;
        TechnicalMessage = technicalMessage;
    }

}
