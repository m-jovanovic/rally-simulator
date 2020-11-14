using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="Vehicle"/> entity configuration.
    /// </summary>
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(vehicle => vehicle.Id);

            builder.OwnsOne(vehicle => vehicle.TeamName, teamNameBuilder =>
                teamNameBuilder
                    .Property(lengthInKilometers => lengthInKilometers.Value)
                    .HasColumnName(nameof(Vehicle.TeamName))
                    .IsRequired());

            builder.OwnsOne(vehicle => vehicle.ModelName, modelNameBuilder =>
                modelNameBuilder
                    .Property(lengthInKilometers => lengthInKilometers.Value)
                    .HasColumnName(nameof(Vehicle.ModelName))
                    .IsRequired());

            builder.Property(vehicle => vehicle.ManufacturingDate).HasColumnType("date").IsRequired();

            builder.Property(vehicle => vehicle.VehicleType).IsRequired();

            builder.Property(vehicle => vehicle.VehicleSubtype).IsRequired();

            builder.Property(vehicle => vehicle.Status).HasDefaultValue(VehicleStatus.Pending).IsRequired();

            builder.Property(vehicle => vehicle.NumberOfHoursLeftUntilRepaired).IsRequired(false);

            builder.OwnsOne(vehicle => vehicle.DistanceCovered, lengthInKilometersBuilder =>
                lengthInKilometersBuilder
                    .Property(lengthInKilometers => lengthInKilometers.Value)
                    .HasColumnName(nameof(Vehicle.DistanceCovered))
                    .IsRequired());

            builder.Property(vehicle => vehicle.StartTimeUtc).IsRequired(false);

            builder.Property(vehicle => vehicle.NumberOfHoursPassedFromRaceStart).IsRequired(false);

            builder.Property(vehicle => vehicle.FinishTimeUtc).IsRequired(false);

            builder.Property(vehicle => vehicle.CreatedOnUtc).IsRequired();

            builder.Property(vehicle => vehicle.ModifiedOnUtc).IsRequired(false);

            builder.Navigation(vehicle => vehicle.DistanceCovered).IsRequired();

            builder.Navigation(vehicle => vehicle.TeamName).IsRequired();

            builder.Navigation(vehicle => vehicle.ModelName).IsRequired();

            builder.HasOne<Race>()
                .WithMany()
                .HasForeignKey(vehicle => vehicle.RaceId)
                .IsRequired();

            builder.HasOne(vehicle => vehicle.Speed)
                .WithMany()
                .HasForeignKey(vehicle => vehicle.VehicleSubtypeId)
                .IsRequired();

            builder.HasOne(vehicle => vehicle.RepairmentLength)
                .WithMany()
                .HasForeignKey(vehicle => vehicle.VehicleTypeId)
                .IsRequired();

            builder.HasOne(vehicle => vehicle.MalfunctionProbability)
                .WithMany()
                .HasForeignKey(vehicle => vehicle.VehicleSubtypeId)
                .IsRequired();

            builder.HasMany(vehicle => vehicle.Malfunctions)
                .WithOne()
                .HasForeignKey(malfunction => malfunction.VehicleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
