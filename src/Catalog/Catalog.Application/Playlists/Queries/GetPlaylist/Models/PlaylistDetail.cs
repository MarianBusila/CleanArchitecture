namespace Catalog.Application.Playlists.Queries.GetPlaylist.Models
{
    public sealed class PlaylistDetail
    {

        /// <summary>
        /// A unique integer based on playlist identifier
        /// </summary>
        /// <example>83488</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of playlist
        /// </summary>
        /// <example>Grunge</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Number of tracks in playlist
        /// </summary>
        /// <example>Grunge</example>
        public int TrackCount { get; set; }
    }
}
