using Microsoft.AspNetCore.Mvc;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackQuery : PagedQueryParams
    {

        /// <summary>
        /// Name of the track
        /// </summary>
        [FromQuery(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// One or more composers
        /// </summary>
        [FromQuery(Name = "composer")]
        public string Composer { get; set; } = string.Empty;

        /// <summary>
        /// Genre of music
        /// </summary>
        [FromQuery(Name = "genre")]
        public string Genre { get; set; } = string.Empty;

        /// <summary>
        /// Music Album
        /// </summary>
        [FromQuery(Name = "album")]
        public string Album { get; set; } = string.Empty;

        /// <summary>
        /// Alum artist
        /// </summary>
        [FromQuery(Name = "artist")]
        public string Artist { get; set; } = string.Empty;

        /// <summary>
        /// The media type
        /// </summary>
        [FromQuery(Name = "media-type")]
        public string MediaType { get; set; } = string.Empty;

        /// <summary>
        /// Specify the minimum price to search by
        /// </summary>
        [FromQuery(Name = "price-from")]
        public string PriceFrom { get; set; }

        /// <summary>
        /// Specify the maximum price to search by
        /// </summary>
        [FromQuery(Name = "price-to")]
        public string PriceTo { get; set; }

        public TrackQuery() : base()
        {
        }
    }
}
