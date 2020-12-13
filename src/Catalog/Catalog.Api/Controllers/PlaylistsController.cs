using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Queries.GetPlaylist;
using Catalog.Application.Playlists.Queries.GetPlaylist.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("v1/playlists")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class PlaylistsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public PlaylistsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet(Name = nameof(GetPlaylists))]
        [ProducesResponseType(typeof(IEnumerable<PlaylistDetail>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlaylists([FromQuery] PlaylistQuery playlistQuery)
        {
            var playlists = await _mediator.Send(new GetPlaylistListQuery(playlistQuery));
            return Ok(playlists);
        }
    }
}
