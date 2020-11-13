using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Vehicles.Commands.CreateVehicle
{
    /// <summary>
    /// Represents the <see cref="CreateVehicleCommand"/> handler.
    /// </summary>
    internal sealed class CreateVehicleCommandHandler : ICommandHandler<CreateVehicleCommand, Result>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public CreateVehicleCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Result> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            Result<TeamName> teamNameResult = TeamName.Create(request.TeamName);
            Result<ModelName> modelNameResult = ModelName.Create(request.ModelName);

            var result = Result.FirstFailureOrSuccess(teamNameResult, modelNameResult);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            Maybe<Race> maybeRace = await _dbContext.GetBydIdAsync<Race>(request.RaceId);

            if (maybeRace.HasNoValue)
            {
                return Result.Failure(ValidationErrors.Race.NotFound);
            }

            Race race = maybeRace.Value;

            if (race.Status != RaceStatus.Pending)
            {
                return Result.Failure(DomainErrors.Race.AlreadyStarted);
            }

            var vehicleSubtype = (VehicleSubtype)request.VehicleSubtype;

            var vehicle = new Vehicle(
                race.Id,
                teamNameResult.Value,
                modelNameResult.Value,
                request.ManufacturingDate,
                vehicleSubtype);

            _dbContext.Insert(vehicle);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
