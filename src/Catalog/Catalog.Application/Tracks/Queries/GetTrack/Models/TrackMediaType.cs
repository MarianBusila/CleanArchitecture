namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackMediaType
    {
        /// <summary>
        /// A unique integer based on media type identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// Name of media type
        /// </summary>
        /// <example>MPEG audio file</example>
        public string Name { get; set; } = string.Empty;
    }
}
