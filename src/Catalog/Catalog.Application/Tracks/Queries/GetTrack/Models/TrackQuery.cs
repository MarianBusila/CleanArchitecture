using System.ComponentModel.DataAnnotations;
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
        [RegularExpression(@"^(gt|gte|lt|lte|eq):[1-9]{1}\d*\.?\d*$", ErrorMessage = "Invalid 'price-from' specified. Value be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5")]
        public string PriceFrom { get; set; }

        /// <summary>
        /// Specify the maximum price to search by
        /// </summary>
        [FromQuery(Name = "price-to")]
        [RegularExpression(@"^(gt|gte|lt|lte|eq):[1-9]{1}\d*\.?\d*$", ErrorMessage = "Invalid 'price-to' specified. Value be in the form of 'gt:10.5', gte:10.5, lt:10.5, lte:10.5, eq:10.5")]
        public string PriceTo { get; set; }

        public TrackQuery() : base()
        {
        }
    }
}
