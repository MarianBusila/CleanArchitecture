using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.AspNetCore.Mvc
{
    public static class IUrlHelperExtensions
    {
        public static Uri LinkCurrentPage(
            this IUrlHelper urlHelper,
            string routeName,
            int pageSize,
            int currentPageNumber,
            PagedQueryParams pagedQueryParams)
        {
            if (currentPageNumber < 1 || pageSize < 1)
                return null;

            return urlHelper.Link(routeName, currentPageNumber, pagedQueryParams);
        }

        public static Uri LinkFirstPage(
            this IUrlHelper urlHelper,
            string routeName,
            int pageSize,
            PagedQueryParams pagedQueryParams)
        {
            if (pageSize < 1)
                return null;

            return urlHelper.Link(routeName, 1, pagedQueryParams);
        }

        public static Uri LinkLastPage(
            this IUrlHelper urlHelper,
            string routeName,
            int pageSize,
            int lastPageNumber,
            PagedQueryParams pagedQueryParams)
        {
            if (lastPageNumber < 1 || pageSize < 1)
                return null;

            return urlHelper.Link(routeName, lastPageNumber, pagedQueryParams);
        }

        public static Uri LinkNextPage(
            this IUrlHelper urlHelper,
            string routeName,
            int pageSize,
            int? nextPageNumber,
            PagedQueryParams pagedQueryParams)
        {
            if (!nextPageNumber.HasValue || nextPageNumber < 1 || pageSize < 1)
                return null;

            return urlHelper.Link(routeName, nextPageNumber.Value, pagedQueryParams);
        }

        public static Uri LinkPreviousPage(
            this IUrlHelper urlHelper,
            string routeName,
            int pageSize,
            int? previousPageNumber,
            PagedQueryParams pagedQueryParams)
        {
            if (!previousPageNumber.HasValue || previousPageNumber < 1 || pageSize < 1)
                return null;

            return urlHelper.Link(routeName, previousPageNumber.Value, pagedQueryParams);
        }

        public static Uri Link(
            this IUrlHelper urlHelper,
            string routeName,
            int pageNumber,
            PagedQueryParams pagedQueryParams)
        {
            if (urlHelper is null)
                throw new ArgumentNullException(nameof(urlHelper));

            if (string.IsNullOrEmpty(routeName))
                throw new ArgumentException("Expected non-null/empty route name", nameof(routeName));

            if (pagedQueryParams is null)
                throw new ArgumentNullException(nameof(pagedQueryParams));

            if (!(pagedQueryParams.GetType()
                .GetProperty(nameof(PagedQueryParams.PageNumber), BindingFlags.Public | BindingFlags.Instance)
                ?.GetCustomAttribute(typeof(FromQueryAttribute)) is FromQueryAttribute pageNumberParam))
                throw new InvalidOperationException($"Expected the property '{nameof(PagedQueryParams.PageNumber)}' to have an attribute of type '{nameof(FromQueryAttribute)}'.");

            var routeValues = pagedQueryParams.ToRouteValuesDictionary();
            routeValues[pageNumberParam.Name] = pageNumber;

            return new Uri(urlHelper.Link(routeName, routeValues));

        }
    }
}
