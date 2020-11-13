using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="VehicleTypeRepairmentLength"/> entity configuration.
    /// </summary>
    internal sealed class VehicleTypeRepairmentLengthConfiguration : IEntityTypeConfiguration<VehicleTypeRepairmentLength>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<VehicleTypeRepairmentLength> builder)
        {
            builder.HasKey(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.Id);

            builder.Property(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.RepairmentLengthInHours).IsRequired();

            builder.Ignore(vehicleTypeRepairmentLength => vehicleTypeRepairmentLength.VehicleType);
        }
    }
}
