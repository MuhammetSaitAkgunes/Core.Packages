using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging
{
    public static class IQueryablePaginateExtensions
    {
        public static async Task<Paginate<T>> ToPaginateAsync<T>(
            this IQueryable<T> source,
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

            return new Paginate<T>
            {
                Size = size,
                Index = index,
                Count = count,
                Pages = (int)Math.Ceiling(count / (double)size),
                Items = items
            };
        }

        //write not async version
        public static Paginate<T> ToPaginate<T>(
            this IQueryable<T> source,
            int index = 0,
            int size = 10)
        {
            var count = source.Count();
            var items = source.Skip(index * size).Take(size).ToList();

            return new Paginate<T>
            {
                Size = size,
                Index = index,
                Count = count,
                Pages = (int)Math.Ceiling(count / (double)size),
                Items = items
            };
        }
    }
}