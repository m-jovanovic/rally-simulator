using System.Collections.Generic;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Extensions
{
    /// <summary>
    /// Contains the extension method for seeding the database with initial data.
    /// </summary>
    public static class SeedExtensions
    {
        /// <summary>
        /// Seeds the database with initial data.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public static void SeedDatabase(this RallySimulatorDbContext dbContext)
        {
            dbContext.Set<VehicleTypeRepairmentLength>().AddRange(new List<VehicleTypeRepairmentLength>
            {
                new VehicleTypeRepairmentLength(VehicleType.Truck, 7),
                new VehicleTypeRepairmentLength(VehicleType.Car, 5),
                new VehicleTypeRepairmentLength(VehicleType.Motorcycle, 3)
            });

            dbContext.Set<VehicleSubtypeSpeed>().AddRange(new List<VehicleSubtypeSpeed>
            {
                new VehicleSubtypeSpeed(VehicleSubtype.Truck, SpeedInKilometersPerHour.Create(80).Value),
                new VehicleSubtypeSpeed(VehicleSubtype.TerrainCar, SpeedInKilometersPerHour.Create(100).Value),
                new VehicleSubtypeSpeed(VehicleSubtype.SportsCar, SpeedInKilometersPerHour.Create(140).Value),
                new VehicleSubtypeSpeed(VehicleSubtype.CrossMotorcycle, SpeedInKilometersPerHour.Create(85).Value),
                new VehicleSubtypeSpeed(VehicleSubtype.SportMotorcycle, SpeedInKilometersPerHour.Create(130).Value),
            });

            dbContext.Set<VehicleSubtypeMalfunctionProbability>().AddRange(new List<VehicleSubtypeMalfunctionProbability>
            {
                new VehicleSubtypeMalfunctionProbability(
                    VehicleSubtype.Truck,
                    MalfunctionProbability.Create(0.06m).Value,
                    MalfunctionProbability.Create(0.04m).Value),
                new VehicleSubtypeMalfunctionProbability(
                    VehicleSubtype.TerrainCar,
                    MalfunctionProbability.Create(0.03m).Value,
                    MalfunctionProbability.Create(0.01m).Value),
                new VehicleSubtypeMalfunctionProbability(
                    VehicleSubtype.SportsCar,
                    MalfunctionProbability.Create(0.12m).Value,
                    MalfunctionProbability.Create(0.02m).Value),
                new VehicleSubtypeMalfunctionProbability(
                    VehicleSubtype.CrossMotorcycle,
                    MalfunctionProbability.Create(0.03m).Value,
                    MalfunctionProbability.Create(0.02m).Value),
                new VehicleSubtypeMalfunctionProbability(
                    VehicleSubtype.SportMotorcycle,
                    MalfunctionProbability.Create(0.18m).Value,
                    MalfunctionProbability.Create(0.10m).Value)
            });

            dbContext.SaveChanges();
        }
    }
}
