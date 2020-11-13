using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives.Maybe;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Vehicles.Commands.RemoveVehicle
{
    /// <summary>
    /// Represents the <see cref="RemoveVehicleCommand"/> handler.
    /// </summary>
    internal sealed class RemoveVehicleCommandHandler : ICommandHandler<RemoveVehicleCommand, Result>
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RemoveVehicleCommandHandler(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<Result> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
        {
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

            _dbContext.Remove(vehicle);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
