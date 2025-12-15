using Microsoft.EntityFrameworkCore;

namespace DAL.Helpers;

public static class PaginationHelper
{
    public static async Task<(List<T> Items, long TotalCount)> PaginateAsync<T>(
        IQueryable<T> query,
        int page = 1,
        int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}