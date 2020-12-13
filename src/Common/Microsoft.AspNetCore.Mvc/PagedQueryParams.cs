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

        

    }
}
