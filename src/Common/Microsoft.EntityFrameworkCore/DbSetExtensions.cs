using System;
using System.Linq;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> TagWithQueryName<T>(this DbSet<T> source, string queryName) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Value may not be null");

            if (string.IsNullOrWhiteSpace(queryName))
                throw new ArgumentException("The specified query name may not be null, empty or whitespace", nameof(queryName));

            return source.TagWith($"QueryName: {queryName}");

        }
    }
}
