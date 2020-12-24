namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackArtist
    {
        /// <summary>
        /// A unique integer based on artist identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// Name of artist
        /// </summary>
        /// <example>Jon Bon Jovi</example>
        public string Name { get; set; } = string.Empty;
    }
}
