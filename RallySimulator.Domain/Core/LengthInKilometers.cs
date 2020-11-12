using System.Collections.Generic;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the length in kilometers.
    /// </summary>
    public sealed class LengthInKilometers : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LengthInKilometers"/> class.
        /// </summary>
        /// <param name="value">The length value.</param>
        private LengthInKilometers(int value) => Value = value;

        /// <summary>
        /// Gets the rally length.
        /// </summary>
        public int Value { get; private set; }

        public static implicit operator int(LengthInKilometers lengthInKilometers) => lengthInKilometers.Value;

        /// <summary>
        /// Creates a new <see cref="LengthInKilometers"/> instance based on the specified value.
        /// </summary>
        /// <param name="length">The length value.</param>
        /// <returns>The result of the length creation process containing the length or an error.</returns>
        public static Result<LengthInKilometers> Create(int length) =>
            Result.Success(length)
                .Ensure(x => x >= 0, DomainErrors.LengthInKilometers.LessThanZero)
                .Map(x => new LengthInKilometers(x));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
