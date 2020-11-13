using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleSubtypeSpeed"/> entity configuration.
    /// </summary>
    internal sealed class VehicleSubtypeSpeedConfiguration : IEntityTypeConfiguration<VehicleSubtypeSpeed>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleSubtypeSpeed> builder)
        {
            builder.HasKey(vehicleSubtypeSpeed => vehicleSubtypeSpeed.Id);

            builder.OwnsOne(
                vehicleSubtypeSpeed => vehicleSubtypeSpeed.SpeedInKilometersPerHour,
                speedInKilometersPerHourBuilder =>
                    speedInKilometersPerHourBuilder
                        .Property(speedInKilometersPerHour => speedInKilometersPerHour.Value)
                        .HasColumnName(nameof(VehicleSubtypeSpeed.SpeedInKilometersPerHour))
                        .IsRequired());

            builder.Navigation(vehicleSubtypeSpeed => vehicleSubtypeSpeed.SpeedInKilometersPerHour).IsRequired();

            builder.Ignore(vehicleSubtypeSpeed => vehicleSubtypeSpeed.VehicleSubtype);
        }
    }
}
