using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Application.Tracks.Queries.GetTrack;
using Catalog.Application.Tracks.Queries.GetTrack.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("v1/tracks")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json", "application/xml")]
    public class TracksController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IUrlHelper _urlHelper;

        public TracksController(IMediator mediator, IUrlHelper urlHelper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        /// <summary>
        /// Get a list of tracks
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /tracks
        ///
        /// </remarks>
        /// <param name="trackQuery"></param>
        /// <returns>Returns a collection of tracks with pagination details in the 'x-pagination' header</returns>
        /// <response code="200">Returns a collection of tracks with pagination details in the 'x-pagination' header</response>
        /// <response code="400">The request could not be understood by the server due to malformed syntax. The client SHOULD NOT repeat the request without modifications</response>
        /// <response code="500">A server fault occurred</response>
        [HttpGet(Name = nameof(GetTracks))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTracks([FromQuery] TrackQuery trackQuery)
        {
            IPagedCollection<TrackDetail> response = await _mediator.Send(new GetTrackListQuery(trackQuery));
            return this.OkWithPageHeader(response, nameof(GetTracks), trackQuery, _urlHelper);
        }

    }
}
