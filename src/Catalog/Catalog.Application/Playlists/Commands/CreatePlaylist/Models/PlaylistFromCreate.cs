namespace Catalog.Application.Playlists.Commands.CreatePlaylist.Models
{
    public sealed class PlaylistFromCreate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
