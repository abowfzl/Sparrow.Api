namespace Sparrow.API.Results.Models;

[Serializable]
public class SparrowApiResponse : SparrowApiResponse<object>
{

    public SparrowApiResponse(object result)
    : base(result)
    {
    }

    public SparrowApiResponse(ErrorInfo error, bool unauthorizedRequest = false)
    : base(error, unauthorizedRequest)
    {
    }
}
