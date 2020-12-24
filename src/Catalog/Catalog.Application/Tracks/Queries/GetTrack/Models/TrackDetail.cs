using System.Runtime.Serialization;

namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackDetail
    {
        /// <summary>
        /// A unique integer based track identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of a track
        /// </summary>
        /// <example>Primal Scream</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The name of a track
        /// </summary>
        /// <example>Primal Scream</example>
        public string Composer { get; set; } = string.Empty;

        /// <summary>
        /// The price of a track
        /// </summary>
        /// <example>0.99</example>
        public decimal UnitPrice { get; set; }

        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// Size of track in bytes
        /// </summary>
        /// <example>242525</example>
        public int SizeInBytes { get; set; }

        /// <summary>
        /// The length of track in human friendly form
        /// </summary>
        /// <example>3 minutes</example>
        public string Time { get; set; } = string.Empty;

        /// <summary>
        /// The length of track
        /// </summary>
        /// <example>1800</example>
        public decimal TimeInMilliseconds { get; set; }

        /// <summary>
        /// The genre
        /// </summary>
        /// <example>Rock</example>
        public TrackGenre Genre { get; set; }

        /// <summary>
        /// The album
        /// </summary>
        /// <example>Razors Edge</example>
        public TrackAlbum Album { get; set; }

        /// <summary>
        /// The artist
        /// </summary>
        /// <example>Razors Edge</example>
        public TrackArtist Artist { get; set; }

        /// <summary>
        /// The media type
        /// </summary>
        /// <example>MPEG audio file</example>
        public TrackMediaType MediaType { get; set; }

    }
}
