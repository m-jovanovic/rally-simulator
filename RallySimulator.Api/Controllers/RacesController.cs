using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallySimulator.Api.Constants;
using RallySimulator.Api.Contracts;
using RallySimulator.Api.Infrastructure;
using RallySimulator.Application.Contracts.Races;
using RallySimulator.Application.Core.Races.Commands.CreateRace;
using RallySimulator.Application.Core.Races.Commands.StartRace;
using RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboard;
using RallySimulator.Application.Core.Races.Queries.GetRaceStatus;
using RallySimulator.Domain.Primitives.Maybe;
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
                .Map(value => new CreateRaceCommand(request.Year))
                .Bind(command => Sender.Send(command))
                .Match(Ok, BadRequest);

        /// <summary>
        /// Starts the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <returns>200 - OK if the race was started successfully, otherwise 400 - Bad Request.</returns>
        [HttpPut(ApiRoutes.Races.StartRace)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartRace(int raceId) =>
            await Result.Success(new StartRaceCommand(raceId))
                .Bind(command => Sender.Send(command))
                .Match(Ok, BadRequest);

        /// <summary>
        /// Gets the race status for the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <returns>200 - OK if the race with the specified identifier exists, otherwise 404 - Not Found.</returns>
        [HttpGet(ApiRoutes.Races.GetRaceStatus)]
        [ProducesResponseType(typeof(RaceStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRaceStatus(int raceId) =>
            await Maybe<GetRaceStatusQuery>.From(new GetRaceStatusQuery(raceId))
                .Bind(command => Sender.Send(command))
                .Match(Ok, NotFound);

        /// <summary>
        /// Gets the race leaderboard for the race with the specified identifier.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <returns>200 - OK if the race with the specified identifier exists, otherwise 404 - Not Found.</returns>
        [HttpGet(ApiRoutes.Races.GetRaceLeaderboard)]
        [ProducesResponseType(typeof(RaceLeaderboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRaceLeaderboard(int raceId) =>
            await Maybe<GetRaceLeaderboardQuery>.From(new GetRaceLeaderboardQuery(raceId))
                .Bind(command => Sender.Send(command))
                .Match(Ok, NotFound);
    }
}
