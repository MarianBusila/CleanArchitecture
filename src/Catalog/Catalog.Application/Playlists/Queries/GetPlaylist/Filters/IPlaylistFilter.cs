namespace Catalog.Application.Playlists.Queries.GetPlaylist.Filters
{
    public interface IPlaylistFilter
    {
        IPlaylistFilter WhereNameLike(string name);
        IPlaylistFilter WhereTrackIdEquals(int? trackId);

    }
}
