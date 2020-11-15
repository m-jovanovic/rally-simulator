using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallySimulator.Api.Constants;
using RallySimulator.Api.Contracts;
using RallySimulator.Api.Infrastructure;
using RallySimulator.Application.Contracts.Common;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Application.Core.Vehicles.Commands.CreateVehicle;
using RallySimulator.Application.Core.Vehicles.Commands.RemoveVehicle;
using RallySimulator.Application.Core.Vehicles.Commands.UpdateVehicle;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleById;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicles;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatistics;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleStatuses;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleSubtypes;
using RallySimulator.Application.Core.Vehicles.Queries.GetVehicleTypes;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Api.Controllers
{
    /// <summary>
    /// Represents the vehicles resource controller.
    /// </summary>
    public sealed class VehiclesController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public VehiclesController(ISender sender)
            : base(sender)
        {
        }

        /// <summary>
        /// Creates the vehicle based on the specified request.
        /// </summary>
        /// <param name="request">The create vehicle request.</param>
        /// <returns>200 - OK if the vehicle was created successfully, otherwise 400 - Bad Request.</returns>
        [HttpPost(ApiRoutes.Vehicles.CreateVehicle)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request) =>
            await Result.Create(request, Errors.UnProcessableRequest)
                .Map(value =>
                    new CreateVehicleCommand(
                        request.RaceId,
                        request.TeamName,
                        request.ModelName,
                        request.ManufacturingDate,
                        request.VehicleSubtype))
                .Bind(command => Sender.Send(command))
                .Match(Ok, BadRequest);

        /// <summary>
        /// Updates the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <param name="request">The update vehicle request.</param>
        /// <returns>200 - OK if the vehicle was updated successfully, otherwise 400 - Bad Request.</returns>
        [HttpPut(ApiRoutes.Vehicles.UpdateVehicle)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVehicle(int vehicleId, [FromBody] UpdateVehicleRequest request) =>
            await Result.Create(request, Errors.UnProcessableRequest)
                .Map(value =>
                    new UpdateVehicleCommand(
                        vehicleId,
                        request.TeamName,
                        request.ModelName,
                        request.ManufacturingDate,
                        request.VehicleSubtype))
                .Bind(command => Sender.Send(command))
                .Match(Ok, BadRequest);

        /// <summary>
        /// Removes the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>204 - No Content if the vehicle was removed successfully, otherwise 400 - Bad Request.</returns>
        [HttpDelete(ApiRoutes.Vehicles.RemoveVehicle)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveVehicle(int vehicleId) =>
            await Result.Success(new RemoveVehicleCommand(vehicleId))
                .Bind(command => Sender.Send(command))
                .Match(NoContent, BadRequest);

        /// <summary>
        /// Gets the vehicle statistics for the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>200 - OK if the vehicle with the specified identifier exists, otherwise 404 - Not Found.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicleById)]
        [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleById(int vehicleId) =>
            await Maybe<GetVehicleByIdQuery>.From(new GetVehicleByIdQuery(vehicleId))
                .Bind(command => Sender.Send(command))
                .Match(Ok, NotFound);

        /// <summary>
        /// Gets the vehicle statistics for the vehicle with the specified identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>200 - OK if the vehicle with the specified identifier exists, otherwise 404 - Not Found.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicleStatistics)]
        [ProducesResponseType(typeof(VehicleStatisticsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehicleStatistics(int vehicleId) =>
            await Maybe<GetVehicleStatisticsQuery>.From(new GetVehicleStatisticsQuery(vehicleId))
                .Bind(command => Sender.Send(command))
                .Match(Ok, NotFound);

        /// <summary>
        /// Gets the vehicles for the specified parameters.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <param name="team">The team.</param>
        /// <param name="model">The model.</param>
        /// <param name="manufacturingDateFrom">The manufacturing date from.</param>
        /// <param name="manufacturingDateTo">The manufacturing date to.</param>
        /// <param name="status">The status.</param>
        /// <param name="distanceFrom">The distance from.</param>
        /// <param name="distanceTo">The distance to.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>200 - OK response with the paged result of vehicles for the specified parameters.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicles)]
        [ProducesResponseType(typeof(PagedList<VehicleResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicles(
            int raceId,
            string team,
            string model,
            DateTime? manufacturingDateFrom,
            DateTime? manufacturingDateTo,
            int status,
            decimal? distanceFrom,
            decimal? distanceTo,
            int page,
            int pageSize,
            string orderBy) =>
            Ok(await Sender.Send(new GetVehiclesQuery(
                raceId,
                team,
                model,
                manufacturingDateFrom,
                manufacturingDateTo,
                status,
                distanceFrom,
                distanceTo,
                page,
                pageSize,
                orderBy)));

        /// <summary>
        /// Gets the vehicle types collection.
        /// </summary>
        /// <returns>200 - OK response with the vehicle types.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicleTypes)]
        [ProducesResponseType(typeof(IReadOnlyCollection<VehicleTypeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleTypes() => Ok(await Sender.Send(new GetVehicleTypesQuery()));

        /// <summary>
        /// Gets the vehicle subtypes collection.
        /// </summary>
        /// <returns>200 - OK response with the vehicle subtypes.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicleSubtypes)]
        [ProducesResponseType(typeof(IReadOnlyCollection<VehicleSubtypeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleSubtypes() => Ok(await Sender.Send(new GetVehicleSubtypesQuery()));

        /// <summary>
        /// Gets the vehicle statuses collection.
        /// </summary>
        /// <returns>200 - OK response with the vehicle statuses.</returns>
        [HttpGet(ApiRoutes.Vehicles.GetVehicleStatuses)]
        [ProducesResponseType(typeof(IReadOnlyCollection<VehicleStatusResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehicleStatuses() => Ok(await Sender.Send(new GetVehicleStatusesQuery()));
    }
}
