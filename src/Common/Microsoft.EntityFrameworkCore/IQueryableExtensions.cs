using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.EntityFrameworkCore
{
    public static class IQueryableExtensions
    {
        public static Task<IPagedCollection<T>> ToPagedCollectionAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Value may not be null");

            if (pageNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Value must be greater than or equal to zero");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Value must be greater than or equal to zero");

            async Task<IPagedCollection<T>> ToPagedCollectionAsync()
            {
                int itemCount = await source.CountAsync(cancellationToken);

                List<T> items = await source
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                return new PagedCollection<T>(items, itemCount, pageNumber, pageSize);
            }

            return ToPagedCollectionAsync();
        }
    }
}
