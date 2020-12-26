namespace Catalog.Application.Tracks.Queries.GetTrack.Filters
{
    public interface ITrackFilter
    {
        ITrackFilter WhereAlbumLike(string album);
        ITrackFilter WhereArtistLike(string artist);
        ITrackFilter WhereComposerLike(string composer);
        ITrackFilter WhereGenreLike(string genre);
        ITrackFilter WhereMediaTypeLike(string mediaType);
        ITrackFilter WhereNameLike(string name);
        ITrackFilter WherePlaylistIdEquals(int? playlistId);
        ITrackFilter WherePrice(string fromPrice, string toPrice);

    }
}
