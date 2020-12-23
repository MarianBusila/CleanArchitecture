using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Commands.CreatePlaylist;
using Catalog.Application.Playlists.Commands.CreatePlaylist.Models;
using Catalog.Application.Playlists.Commands.DeletePlaylist;
using Catalog.Application.Playlists.Commands.UpdatePlaylist;
using Catalog.Application.Playlists.Commands.UpdatePlaylist.Models;
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
        private readonly IUrlHelper _urlHelper;

        public PlaylistsController(IMediator mediator, IUrlHelper urlHelper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        [HttpGet(Name = nameof(GetPlaylists))]
        [ProducesResponseType(typeof(IPagedCollection<PlaylistDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlaylists([FromQuery] PlaylistQuery playlistQuery)
        {
            IPagedCollection<PlaylistDetail> playlists = await _mediator.Send(new GetPlaylistListQuery(playlistQuery));
            return this.OkWithPageHeader(playlists, nameof(GetPlaylists), playlistQuery, _urlHelper);
        }

        [HttpGet("{playlistId:int}", Name = nameof(GetPlaylistById))]
        [ProducesResponseType(typeof(PlaylistDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlaylistById(int playlistId)
        {
            PlaylistDetail playlist = await _mediator.Send(new GetPlaylistQuery(playlistId));
            if (playlist == null)
                return NotFound();

            return Ok(playlist);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePlaylist([FromBody]PlaylistForCreate playlist)
        {
            var playlistFromCreate = await _mediator.Send(new CreatePlaylistCommand(playlist));
            return CreatedAtAction(nameof(GetPlaylistById), new { playlistId = playlistFromCreate.Id }, playlistFromCreate);
        }

        [HttpPut("{playlistId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePlaylist(int playlistId, [FromBody]PlaylistForUpdate playlist)
        {
            await _mediator.Send(new UpdatePlaylistCommand(playlistId, playlist));
            return Ok();
        }

        [HttpDelete("{playlistId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePlaylist(int playlistId)
        {
            await _mediator.Send(new DeletePlaylistCommand(playlistId));
            return NoContent();
        }
    }
}
