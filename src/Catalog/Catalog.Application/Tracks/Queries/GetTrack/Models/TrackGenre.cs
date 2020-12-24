namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackGenre
    {
        /// <summary>
        /// A unique integer based on genre identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// Name of genre
        /// </summary>
        /// <example>Rock</example>
        public string Name { get; set; } = string.Empty;
    }
}
