using System.Collections.Generic;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;
using RallySimulator.Domain.Utility;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the speed in kilometers per hour.
    /// </summary>
    public sealed class SpeedInKilometersPerHour : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedInKilometersPerHour"/> class.
        /// </summary>
        /// <param name="value">The speed value.</param>
        private SpeedInKilometersPerHour(int value) => Value = value;

        /// <summary>
        /// Gets the speed value.
        /// </summary>
        public int Value { get; private set; }

        public static LengthInKilometers operator *(SpeedInKilometersPerHour speed, int numberOfHours)
        {
            Ensure.GreaterThanOrEqualToZero(
                numberOfHours,
                "The number of hours must greater than or equal to zero",
                nameof(numberOfHours));

            return LengthInKilometers.Create(speed.Value * numberOfHours).Value;
        }

        /// <summary>
        /// Creates a new <see cref="SpeedInKilometersPerHour"/> instance based on the specified value.
        /// </summary>
        /// <param name="speed">The speed value.</param>
        /// <returns>The result of the speed creation process containing the speed or an error.</returns>
        public static Result<SpeedInKilometersPerHour> Create(int speed) =>
            Result.Success(speed)
                .Ensure(x => x > 0, DomainErrors.SpeedInKilometersPerHour.LessThanOrEqualToZero)
                .Map(x => new SpeedInKilometersPerHour(x));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
