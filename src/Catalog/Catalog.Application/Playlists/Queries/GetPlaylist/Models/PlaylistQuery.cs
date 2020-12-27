using Microsoft.AspNetCore.Mvc;

namespace Catalog.Application.Playlists.Queries.GetPlaylist.Models
{
    public sealed class PlaylistQuery : PagedQueryParams
    {

        /// <summary>
        /// Name of playlist
        /// </summary>
        [FromQuery(Name = "name")]
        public string Name { get; set; } = string.Empty;

        public PlaylistQuery() : base()
        {
        }

    }
}
