namespace Sparrow.Core;

public interface IPagedList<T> : IList<T>
{
    int TotalCount { get; }

    int TotalPages { get; }
}
