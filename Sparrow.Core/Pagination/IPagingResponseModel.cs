namespace Sparrow.Core.Pagination;

public interface IPagingResponseModel
{
    public int TotalCount { get; }

    public int TotalPages { get; }
}
