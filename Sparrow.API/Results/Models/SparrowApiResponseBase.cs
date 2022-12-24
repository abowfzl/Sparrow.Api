namespace Sparrow.API.Results.Models
{
    public abstract class SparrowApiResponseBase
    {
        public bool Success { get; set; }

        public bool UnauthorizedRequest { get; set; }

        public bool __wrapped { get; } = true;

        public ErrorInfo? Error { get; set; }
    }

}
