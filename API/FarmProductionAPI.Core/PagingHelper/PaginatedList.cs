using Microsoft.EntityFrameworkCore;
namespace FarmProductionAPI.Core.PagingHelper
{
    public class PaginatedList<T>
    {

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalCount = count;
            PageSize = pageSize;

            Items = new List<T>();
            Items.AddRange(items);
        }
        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public List<T> Items { get; set; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = pageSize > 0
                ? source.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                : source;

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
