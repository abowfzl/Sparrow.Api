namespace Sparrow.Core.Pagination;

public abstract class BasePagingRequest : IPagingRequestModel
{
    public int PageIndex { get; set; } = 0;

    public int PageSize { get; set; } = 10;
}
