namespace Sparrow.API.Results.Models
{
    [Serializable]
    public class SparrowApiResponse<TResult> : SparrowApiResponseBase
    {
        public TResult? Result { get; set; }

        public SparrowApiResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        public SparrowApiResponse(ErrorInfo error, bool unauthorizedRequest = false)
        {
            Error = error;
            UnauthorizedRequest = unauthorizedRequest;
            Success = false;
        }
    }
}
