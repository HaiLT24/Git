using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.DataAccessLayer.Common
{
    public class PaginatedList<TEntity> : List<TEntity>
    {
        //Total record
        public new int Count { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        // pageIndex = 1 => false
        // pageIndex = 2 => true
        public bool HasPreviousPage => PageIndex > 1;

        // pageIndex = 1 => true
        // pageIndex = 6 => false
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            Count = count;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            AddRange(items);
        }

        public static async Task<PaginatedList<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageIndex,
            int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<TEntity>(items, count, pageIndex, pageSize);
        }
    }
}
