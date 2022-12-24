using Microsoft.EntityFrameworkCore;
using Sparrow.Core;

namespace Sparrow.Data.Extentios;

public static class AsyncIQueryableExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken, bool getOnlyTotalCount = false)
    {
        if (source == null)
            return new PagedList<T>(new List<T>(), pageIndex, pageSize);

        //min allowed page size is 1
        pageSize = Math.Max(pageSize, 1);

        var count = await source.CountAsync(cancellationToken);

        var data = new List<T>();

        if (!getOnlyTotalCount)
            data.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken));

        return new PagedList<T>(data, pageIndex, pageSize, count);
    }
}
