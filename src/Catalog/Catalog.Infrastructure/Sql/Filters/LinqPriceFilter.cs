using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Catalog.Domain.Models;
using Common.Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Sql.Filters
{
    public sealed class LinqPriceFilter : EqualityFilterBase<Track, decimal>
    {

        public LinqPriceFilter(string filterExpression) : base(filterExpression)
        {
        }

        protected override IDictionary<string, Expression<Func<Track, bool>>> CreateEqualityOperatorExpressionLookup()
        {
            return new Dictionary<string, Expression<Func<Track, bool>>>
            {
                {"gt", e => e.UnitPrice > Value},
                {"gte", e => e.UnitPrice >= Value},
                {"lt", e => e.UnitPrice < Value},
                {"lte", e => e.UnitPrice <= Value},
                {"eq", e => e.UnitPrice == Value}
            };
        }

    }
}
