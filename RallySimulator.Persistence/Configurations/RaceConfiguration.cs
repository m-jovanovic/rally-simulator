using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RallySimulator.Domain.Core;

namespace RallySimulator.Persistence.Configurations
{
    /// <summary>
    /// Contains the <see cref="Race"/> entity configuration.
    /// </summary>
    internal sealed class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(race => race.Id);

            builder.Property(race => race.Year).IsRequired();

            builder.OwnsOne(race => race.LengthInKilometers, lengthInKilometersBuilder =>
                lengthInKilometersBuilder
                    .Property(lengthInKilometers => lengthInKilometers.Value)
                    .HasColumnName(nameof(Race.LengthInKilometers))
                    .IsRequired());

            builder.Property(race => race.Status).HasDefaultValue(RaceStatus.Pending).IsRequired();

            builder.Navigation(race => race.LengthInKilometers).IsRequired();

            builder.Property(race => race.StartTimeUtc).IsRequired(false);

            builder.Property(race => race.FinishTimeUtc).IsRequired(false);

            builder.Property(race => race.CreatedOnUtc).IsRequired();

            builder.Property(race => race.ModifiedOnUtc).IsRequired(false);

            builder.HasMany<Vehicle>()
                .WithOne()
                .HasForeignKey(vehicle => vehicle.RaceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
