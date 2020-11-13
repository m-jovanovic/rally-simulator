using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RallySimulator.Api.Constants;
using RallySimulator.Api.Contracts;
using RallySimulator.Api.Infrastructure;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Application.Core.Vehicles.Commands.CreateVehicle;
using RallySimulator.Application.Core.Vehicles.Commands.RemoveVehicle;
using RallySimulator.Application.Core.Vehicles.Commands.UpdateVehicle;
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
    }
}
