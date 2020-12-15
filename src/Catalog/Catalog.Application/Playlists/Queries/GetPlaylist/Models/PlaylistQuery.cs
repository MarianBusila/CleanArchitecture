using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Application.Playlists.Queries.GetPlaylist.Models
{
    public sealed class PlaylistQuery : PagedQueryParams
    {
        private const string DEFAULT_ORDER = "name"; // order by name ascending

        /// <summary>
        /// Name of playlist
        /// </summary>
        [FromQuery(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A sort expression
        /// </summary>
        /// <remarks>
        /// The default sort order is ascending. Use a '-' at the start of expression to order descending. The sort value may use 'kebab case' AKA 'hyphen case'. Some examples of valid order expressions are 'name', and '-name'
        /// </remarks>
        [FromQuery(Name = "name")]
        [RegularExpression(@"^((-)?[a-zA-Z][a-zA-Z]*)(-[a-zA-Z]+)*((,|,\s|\s,|\s,\s)?((-)?[a-zA-Z][a-zA-Z]*)(-[a-zA-Z]+)*)$", ErrorMessage = "The specified order is invalid. The default sort order is ascending. Use a '-' at the start of expression to order descending. The sort value may use 'kebab case' AKA 'hyphen case'. Some examples of valid order expressions are 'name', and '-name'")] //TODO replace with a validator
        public string Order { get; set; } = DEFAULT_ORDER;

        public PlaylistQuery() : base()
        {
        }

    }
}
