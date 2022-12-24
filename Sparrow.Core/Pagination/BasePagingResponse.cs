namespace Sparrow.Core.Pagination;

public abstract class BasePagingResponse : IPagingResponseModel
{
    public int TotalCount { get; set; }

    public int TotalPages { get; set; }
}
