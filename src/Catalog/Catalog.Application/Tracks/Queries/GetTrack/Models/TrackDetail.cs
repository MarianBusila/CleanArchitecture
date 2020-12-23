namespace Catalog.Application.Tracks.Queries.GetTrack.Models
{
    public sealed class TrackDetail
    {
        /// <summary>
        /// A unique integer based track identifier
        /// </summary>
        /// <example>2342349</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of a track
        /// </summary>
        /// <example>Primal Scream</example>
        public string Name { get; set; } = string.Empty;

        // TODO

    }
}
