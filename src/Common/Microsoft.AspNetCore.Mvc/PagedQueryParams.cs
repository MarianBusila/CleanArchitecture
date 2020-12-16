using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Microsoft.AspNetCore.Mvc
{
    public class PagedQueryParams
    {
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_PAGE_SIZE = 10;

        /// <summary>
        /// A page number having a numeric value of 1 or greater
        /// </summary>
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = DEFAULT_PAGE_NUMBER;

        /// <summary>
        /// A page size having a numeric value of 1 or greater. Represents the number of items returned per page.
        /// </summary>
        [FromQuery(Name = "limit")]
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;

        public PagedQueryParams() : base()
        {
        }

        public virtual IDictionary<string, object> ToRouteValuesDictionary()
        {
            var routeValues = new Dictionary<string, object>();

            var properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)
                ?? Array.Empty<PropertyInfo>();

            foreach (var property in properties)
            {
                var value = property.GetValue(this);

                if (value == null)
                    continue;

                if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
                    continue;

                if (!(property.GetCustomAttribute(typeof(FromQueryAttribute)) is FromQueryAttribute fromQueryAttribute))
                    continue;

                if (value is DateTimeOffset dateTimeOffset)
                {
                    routeValues.Add(fromQueryAttribute.Name, dateTimeOffset.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
                else if (value is DateTime dateTime)
                {
                    routeValues.Add(fromQueryAttribute.Name, dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
                else
                {
                    routeValues.Add(fromQueryAttribute.Name, value);
                }
            }

            return routeValues;
        }


    }
}
