using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Vehicles.Commands.UpdateVehicle
{
    /// <summary>
    /// Represents the <see cref="UpdateVehicleCommand"/> handler.
    /// </summary>
    internal sealed class UpdateVehicleCommandHandler : ICommandHandler<UpdateVehicleCommand, Result>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UpdateVehicleCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<TeamName> teamNameResult = TeamName.Create(request.TeamName);
            Result<ModelName> modelNameResult = ModelName.Create(request.ModelName);

            var result = Result.FirstFailureOrSuccess(teamNameResult, modelNameResult);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            Maybe<Vehicle> maybeVehicle = await _dbContext.GetBydIdAsync<Vehicle>(request.VehicleId);

            if (maybeVehicle.HasNoValue)
            {
                return Result.Failure(ValidationErrors.Vehicle.NotFound);
            }

            Vehicle vehicle = maybeVehicle.Value;

            Race race = (await _dbContext.GetBydIdAsync<Race>(vehicle.RaceId)).Value;

            if (race.Status != RaceStatus.Pending)
            {
                return Result.Failure(DomainErrors.Vehicle.RaceHasStarted);
            }

            Result updateInformationResult = vehicle.UpdateInformation(
                teamNameResult.Value, modelNameResult.Value, request.ManufacturingDate, (VehicleSubtype)request.VehicleSubtype);

            if (updateInformationResult.IsFailure)
            {
                return Result.Failure(updateInformationResult.Error);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
