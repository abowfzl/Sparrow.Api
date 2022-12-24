namespace Sparrow.Core.Pagination;

public interface IPagingRequestModel
{
    public int PageIndex { get; }

    public int PageSize { get; }
}
