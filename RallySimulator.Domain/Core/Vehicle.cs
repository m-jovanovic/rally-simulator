using System;
using System.Collections.Generic;
using System.Linq;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle entity.
    /// </summary>
    public sealed class Vehicle : Entity, IAuditableEntity
    {
        private readonly List<Malfunction> _malfunctions = new List<Malfunction>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="race">The race.</param>
        /// <param name="teamName">The team name.</param>
        /// <param name="modelName">The model name.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        public Vehicle(
            Race race,
            TeamName teamName,
            ModelName modelName,
            DateTime manufacturingDate,
            VehicleSubtype vehicleSubtype)
        {
            Ensure.NotNull(race, "The race is required.", nameof(race));
            Ensure.NotNull(teamName, "The team name is required.", nameof(teamName));
            Ensure.NotNull(modelName, "The model name is required.", nameof(modelName));
            Ensure.NotEmpty(manufacturingDate, "The manufacturing date is required", nameof(manufacturingDate));

            RaceId = race.Id;
            TeamName = teamName;
            ModelName = modelName;
            ManufacturingDate = manufacturingDate.Date;
            VehicleType = DetermineVehicleType(vehicleSubtype);
            VehicleSubtype = vehicleSubtype;
            Status = VehicleStatus.Pending;
            DistanceCovered = LengthInKilometers.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private Vehicle()
        {
        }

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; private set; }

        /// <summary>
        /// Gets the team name.
        /// </summary>
        public TeamName TeamName { get; private set; }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public ModelName ModelName { get; private set; }

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; private set; }

        /// <summary>
        /// Gets the vehicle type.
        /// </summary>
        public VehicleType VehicleType { get; private set; }

        /// <summary>
        /// Gets the vehicle subtype.
        /// </summary>
        public VehicleSubtype VehicleSubtype { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public VehicleStatus Status { get; private set; }

        /// <summary>
        /// Gets the date and time the repair completes on, in UTC format.
        /// </summary>
        public DateTime? RepairCompletesOnUtc { get; private set; }

        /// <summary>
        /// Gets the distance in kilometers the vehicle has covered.
        /// </summary>
        public LengthInKilometers DistanceCovered { get; private set; }

        /// <summary>
        /// Gets the race start time in UTC format.
        /// </summary>
        public DateTime? StartTimeUtc { get; private set; }

        /// <summary>
        /// Gets the race finish time in UTC format.
        /// </summary>
        public DateTime? FinishTimeUtc { get; private set; }

        /// <summary>
        /// Gets the repairment length.
        /// </summary>
        public VehicleTypeRepairmentLength RepairmentLength { get; private set; }

        /// <summary>
        /// Gets the vehicle speed.
        /// </summary>
        public VehicleSubtypeSpeed Speed { get; private set; }

        /// <summary>
        /// Gets the malfunction probability.
        /// </summary>
        public VehicleSubtypeMalfunctionProbability MalfunctionProbability { get; private set; }

        /// <summary>
        /// Gets the malfunctions.
        /// </summary>
        public IReadOnlyCollection<Malfunction> Malfunctions => _malfunctions.ToList();

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <summary>
        /// Gets a value indicating whether or not the vehicle is pending for race start.
        /// </summary>
        private bool Pending => Status == VehicleStatus.Pending;

        /// <summary>
        /// Gets a value indicating whether or not the vehicle is waiting for repair.
        /// </summary>
        private bool WaitingForRepair => Status == VehicleStatus.WaitingForRepair && RepairCompletesOnUtc.HasValue;

        /// <summary>
        /// Gets a value indicating whether or not the vehicle is broken.
        /// </summary>
        private bool Broken => Status == VehicleStatus.Broken;

        /// <summary>
        /// Updates the vehicle information.
        /// </summary>
        /// <param name="teamName">The team name.</param>
        /// <param name="modelName">The model name.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        /// <returns>The success result if the update operation was successful, otherwise a failure result with an error.</returns>
        public Result UpdateInformation(
            TeamName teamName,
            ModelName modelName,
            DateTime manufacturingDate,
            VehicleSubtype vehicleSubtype)
        {
            if (!Pending)
            {
                return Result.Failure(DomainErrors.Vehicle.RaceHasStarted);
            }

            Ensure.NotNull(teamName, "The team name is required.", nameof(teamName));
            Ensure.NotNull(modelName, "The model name is required.", nameof(modelName));
            Ensure.NotEmpty(manufacturingDate, "The manufacturing date is required", nameof(manufacturingDate));

            TeamName = teamName;
            ModelName = modelName;
            ManufacturingDate = manufacturingDate.Date;
            VehicleType = DetermineVehicleType(vehicleSubtype);
            VehicleSubtype = vehicleSubtype;

            return Result.Success();
        }

        /// <summary>
        /// Adds a light malfunction to the vehicle.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        /// <returns>The success result if the operation was successful, otherwise a failure result with an error.</returns>
        public Result AddLightMalfunction(DateTime utcNow)
        {
            Result result = CheckIfVehicleCanSufferMalfunction();

            if (result.IsFailure)
            {
                return result;
            }

            AddMalfunction(MalfunctionType.Light);

            Status = VehicleStatus.WaitingForRepair;

            RepairCompletesOnUtc = utcNow.AddHours(RepairmentLength.RepairmentLengthInHours);

            return Result.Success();
        }

        /// <summary>
        /// Adds a heavy malfunction to the vehicle.
        /// </summary>
        /// <returns>The success result if the operation was successful, otherwise a failure result with an error.</returns>
        public Result AddHeavyMalfunction()
        {
            Result result = CheckIfVehicleCanSufferMalfunction();

            if (result.IsFailure)
            {
                return result;
            }

            AddMalfunction(MalfunctionType.Heavy);

            Status = VehicleStatus.Broken;

            return Result.Success();
        }

        /// <summary>
        /// Changes the distance the vehicle has covered.
        /// </summary>
        /// <param name="distance">The new distance.</param>
        public void ChangeDistance(LengthInKilometers distance) => DistanceCovered = distance;

        /// <summary>
        /// Starts the race for the vehicle.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        public void StartRace(DateTime utcNow)
        {
            // TODO: Add validation?
            Status = VehicleStatus.Racing;

            StartTimeUtc = utcNow;
        }

        /// <summary>
        /// Repairs the vehicle.
        /// </summary>
        public void Repair()
        {
            // TODO: Add validation?
            Status = VehicleStatus.Racing;

            RepairCompletesOnUtc = null;
        }

        /// <summary>
        /// Completes the race for the vehicle.
        /// </summary>
        /// <param name="utcNow">The current date and time in UTC format.</param>
        public void CompleteRace(DateTime utcNow)
        {
            // TODO: Add validation?
            Status = VehicleStatus.CompletedRace;

            FinishTimeUtc = utcNow;
        }

        /// <summary>
        /// Determines the vehicle type based on the specified vehicle subtype.
        /// </summary>
        /// <param name="vehicleSubtype">The vehicle subtype.</param>
        /// <returns>The vehicle type based on the specified vehicle subtype.</returns>
        private static VehicleType DetermineVehicleType(VehicleSubtype vehicleSubtype) =>
            vehicleSubtype switch
            {
                VehicleSubtype.Truck => VehicleType.Truck,
                VehicleSubtype.TerrainCar => VehicleType.Car,
                VehicleSubtype.SportsCar => VehicleType.Car,
                VehicleSubtype.CrossMotorcycle => VehicleType.Motorcycle,
                VehicleSubtype.SportMotorcycle => VehicleType.Motorcycle,
                _ => throw new ArgumentOutOfRangeException(nameof(vehicleSubtype), vehicleSubtype, null)
            };

        /// <summary>
        /// Adds the malfunction of the specified type to the vehicle.
        /// </summary>
        /// <param name="malfunctionType">The malfunction type.</param>
        private void AddMalfunction(MalfunctionType malfunctionType) => _malfunctions.Add(new Malfunction(this, malfunctionType));

        /// <summary>
        /// Checks if the vehicle can suffer a malfunction.
        /// </summary>
        /// <returns>The success result if the vehicle can suffer a malfunction, otherwise a failure result and an error.</returns>
        private Result CheckIfVehicleCanSufferMalfunction()
        {
            if (Pending)
            {
                return Result.Failure(DomainErrors.Vehicle.RaceHasNotStarted);
            }

            if (Broken)
            {
                return Result.Failure(DomainErrors.Vehicle.Broken);
            }

            if (WaitingForRepair)
            {
                return Result.Failure(DomainErrors.Vehicle.WaitingForRepair);
            }

            return Result.Success();
        }
    }
}
