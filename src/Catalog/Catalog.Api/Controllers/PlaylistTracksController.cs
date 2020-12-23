using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Playlists.Commands.AddTracksToPlaylist;
using Catalog.Application.Playlists.Commands.DeleteTracksFromPlaylist;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Catalog.Api.Controllers
{
    [Route("v1/playlists/{playlistId:int}/tracks")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class PlaylistTracksController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IUrlHelper _urlHelper;

        public PlaylistTracksController(IMediator mediator, IUrlHelper urlHelper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        /// <summary>
        /// Add a list of tracks to an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /playlists/{playlistId:int}/tracks/1,2,3,4
        /// 
        /// </remarks>
        /// <param name="playlistId"> Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="404">Resource could not be found for specified playlist id</response>
        /// <response code="500">A server fault occurred</response>
        [HttpPut("{trackIds}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddTracksToPlaylist(
            [FromRoute]int playlistId,
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))] IReadOnlyCollection<int> trackIds)
        {
            await _mediator.Send(new AddTracksToPlaylistCommand(playlistId, trackIds));
            return NoContent();
        }

        /// <summary>
        /// Delete a list of tracks from an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /playlists/{playlistId:int}/tracks/1,2,3,4
        /// 
        /// </remarks>
        /// <param name="playlistId"> Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <response code="204">No content</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="404">Resource could not be found for specified playlist id</response>
        /// <response code="500">A server fault occurred</response>
        [HttpDelete("{trackIds}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteTracksFromPlaylist(
            [FromRoute] int playlistId,
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IReadOnlyCollection<int> trackIds)
        {
            await _mediator.Send(new DeleteTracksFromPlaylistCommand(playlistId, trackIds));
            return NoContent();
        }
    }
}
