using System.Collections.Generic;

namespace Catalog.Application.Playlists.Commands.UpdatePlaylist.Models
{
    public sealed class PlaylistForUpdate
    {

        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<int> TrackIds { get; set; } = new List<int>();

    }
}
