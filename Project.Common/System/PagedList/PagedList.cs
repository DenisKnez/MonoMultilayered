using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Common.System
{
    public class PagedList<TEntity> : List<TEntity>, IPagedList<TEntity>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<TEntity> items, int totalCount, int pageNumber, int pageSize)
        {
            AddRange(items);

            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public async static Task<IPagedList<TEntity>> ToPagedListAsync(IQueryable<TEntity> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}