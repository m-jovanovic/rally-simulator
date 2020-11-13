using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RallySimulator.Persistence.Extensions
{
    /// <summary>
    /// Contains extensions methods for the <see cref="ModelBuilder"/> class.
    /// </summary>
    internal static class ModelBuilderExtensions
    {
        private static readonly ValueConverter<DateTime, DateTime> UtcValueConverter =
            new ValueConverter<DateTime, DateTime>(outside => outside, inside => DateTime.SpecifyKind(inside, DateTimeKind.Utc));

        /// <summary>
        /// Applies the UTC date-time converter to all of the properties that are <see cref="DateTime"/> and end with Utc.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        internal static void ApplyUtcDateTimeConverter(this ModelBuilder modelBuilder) =>
            modelBuilder.Model.GetEntityTypes()
                .ForEach(mutableEntityType => mutableEntityType
                    .GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) && p.Name.EndsWith("Utc", StringComparison.Ordinal))
                    .ForEach(mutableProperty => mutableProperty.SetValueConverter(UtcValueConverter)));
    }
}
