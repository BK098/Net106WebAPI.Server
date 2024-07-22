using Microsoft.EntityFrameworkCore;

namespace Application.Services.Models.Base
{
    public class PaginatedList<TEntity>
    {
        public List<TEntity> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public PaginatedList(List<TEntity> items, int pageIndex, int pageSize, int count)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<TEntity>> CreateAsync(
            IQueryable<TEntity> source, int pageIndex, int pageSize, CancellationToken cancellation)
        {
            var count = await source.CountAsync(cancellation);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellation);
            return new(items, pageIndex, pageSize, count);
        }
    }
}