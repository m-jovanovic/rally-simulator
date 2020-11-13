using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallySimulator.Api.Constants;
using RallySimulator.Api.Contracts;
using RallySimulator.Api.Infrastructure;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Application.Core.Races.Commands.CreateRace;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Api.Controllers
{
    /// <summary>
    /// Represents the races resource controller.
    /// </summary>
    public sealed class RacesController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RacesController"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public RacesController(ISender sender)
            : base(sender)
        {
        }

        /// <summary>
        /// Creates the race based on the specified request.
        /// </summary>
        /// <param name="request">The create race request.</param>
        /// <returns>200 - OK if the race was created successfully, otherwise 400 - Bad Request.</returns>
        [HttpPost(ApiRoutes.Races.CreateRace)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRace([FromBody] CreateRaceRequest request) =>
            await Result.Create(request, Errors.UnProcessableRequest)
                .Map(value => new CreateRaceCommand(request.Year, request.Length))
                .Bind(command => Sender.Send(command))
                .Match(Ok, BadRequest);
    }
}
