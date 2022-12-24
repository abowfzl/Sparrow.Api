namespace Sparrow.Core;

[Serializable]
public class PagedList<T> : List<T>, IPagedList<T>
{
    public int TotalCount { get; }

    public int TotalPages { get; }

    public PagedList(IList<T> source, int pageIndex, int pageSize, int? totalCount = null)
    {
        //min allowed page size is 1
        pageSize = Math.Max(pageSize, 1);

        TotalCount = totalCount ?? source.Count;
        TotalPages = TotalCount / pageSize;

        if (TotalCount % pageSize > 0)
            TotalPages++;

        AddRange(totalCount != null ? source : source.Skip(pageIndex * pageSize).Take(pageSize));
    }
}
