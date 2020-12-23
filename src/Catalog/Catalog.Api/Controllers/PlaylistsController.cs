﻿using System;
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

        /// <summary>
        /// Get a list of playlists
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /playlists
        ///
        /// </remarks>
        /// <param name="playlistQuery"></param>
        /// <returns>Returns a collection of playlists with pagination details in the 'x-pagination' header</returns>
        /// <response code="200">Returns a collection of playlists with pagination details in the 'x-pagination' header</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="500">A server fault occurred</response>

        [HttpGet(Name = nameof(GetPlaylists))]
        [ProducesResponseType(typeof(IPagedCollection<PlaylistDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPlaylists([FromQuery] PlaylistQuery playlistQuery)
        {
            IPagedCollection<PlaylistDetail> playlists = await _mediator.Send(new GetPlaylistListQuery(playlistQuery));
            return this.OkWithPageHeader(playlists, nameof(GetPlaylists), playlistQuery, _urlHelper);
        }

        /// <summary>
        /// Get a single playlist based on specified playlist id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /playlists/1000
        ///
        /// </remarks>
        /// <param name="playlistId">The playlist id</param>
        /// <returns>Returns a single playlist based on specified playlist id</returns>
        /// <response code="200">Returns a single playlist based on specified playlist id</response>
        /// <response code="404">A playlist could not be found for specified playlist id</response>
        /// <response code="500">A server fault occurred</response>

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

        /// <summary>
        /// Create a new playlist
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns>New playlist</returns>
        /// <response code="201">Returns new playlist</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="500">A server fault occurred</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePlaylist([FromBody]PlaylistForCreate playlist)
        {
            var playlistFromCreate = await _mediator.Send(new CreatePlaylistCommand(playlist));
            return CreatedAtAction(nameof(GetPlaylistById), new { playlistId = playlistFromCreate.Id }, playlistFromCreate);
        }

        /// <summary>
        /// Update playlist having specified playlist id
        /// </summary>
        /// <param name="playlistId"></param>
        /// <param name="playlist"></param>
        /// <returns>New playlist</returns>
        /// <response code="204">No content</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="404">A playlist could not be found for specified playlist id</response>
        /// <response code="500">A server fault occurred</response>
        [HttpPut("{playlistId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePlaylist(int playlistId, [FromBody]PlaylistForUpdate playlist)
        {
            await _mediator.Send(new UpdatePlaylistCommand(playlistId, playlist));
            return Ok();
        }

        /// <summary>
        /// Delete playlist by playlist id
        /// </summary>
        /// <param name="playlistId">Playlist id</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="404">A playlist could not be found for specified playlist id</response>
        /// <response code="500">A server fault occurred</response>

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
