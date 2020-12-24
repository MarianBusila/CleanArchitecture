namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackAlbum
    {
        /// <summary>
        /// A unique integer based on album identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// Title of album
        /// </summary>
        /// <example>Let There Be Rock</example>
        public string Title { get; set; } = string.Empty;
    }
}
