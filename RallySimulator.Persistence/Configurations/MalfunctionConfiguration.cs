using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="Malfunction"/> entity configuration.
    /// </summary>
    internal sealed class MalfunctionConfiguration : IEntityTypeConfiguration<Malfunction>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Malfunction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(malfunction => malfunction.VehicleId).IsRequired();

            builder.Property(malfunction => malfunction.Type).IsRequired();

            builder.Property(malfunction => malfunction.CreatedOnUtc).IsRequired();

            builder.Property(malfunction => malfunction.ModifiedOnUtc).IsRequired(false);
        }
    }
}
