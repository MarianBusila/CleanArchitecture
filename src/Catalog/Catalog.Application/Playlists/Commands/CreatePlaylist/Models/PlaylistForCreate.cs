using System.Collections.Generic;

namespace Catalog.Application.Playlists.Commands.CreatePlaylist.Models
{
    public sealed class PlaylistForCreate
    {

        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<int> TrackIds { get; set; } = new List<int>();

    }
}
