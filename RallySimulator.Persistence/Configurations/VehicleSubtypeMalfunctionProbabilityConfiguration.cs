using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleSubtypeMalfunctionProbability"/> entity configuration.
    /// </summary>
    internal sealed class VehicleSubtypeMalfunctionProbabilityConfiguration : IEntityTypeConfiguration<VehicleSubtypeMalfunctionProbability>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleSubtypeMalfunctionProbability> builder)
        {
            builder.HasKey(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.Id);

            builder.OwnsOne(
                vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.LightMalfunctionProbability,
                malfunctionProbabilityBuilder =>
                    malfunctionProbabilityBuilder
                        .Property(malfunctionProbability => malfunctionProbability.Value)
                        .HasColumnName(nameof(VehicleSubtypeMalfunctionProbability.LightMalfunctionProbability))
                        .IsRequired());

            builder.OwnsOne(
                vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.HeavyMalfunctionProbability,
                malfunctionProbabilityBuilder =>
                    malfunctionProbabilityBuilder
                        .Property(malfunctionProbability => malfunctionProbability.Value)
                        .HasColumnName(nameof(VehicleSubtypeMalfunctionProbability.HeavyMalfunctionProbability))
                        .IsRequired());

            builder.Navigation(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.LightMalfunctionProbability)
                .IsRequired();

            builder.Navigation(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.HeavyMalfunctionProbability)
                .IsRequired();

            builder.Ignore(vehicleSubtypeMalfunctionProbability => vehicleSubtypeMalfunctionProbability.VehicleSubtype);
        }
    }
}
