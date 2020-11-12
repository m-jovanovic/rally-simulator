using System.Collections.Generic;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the malfunction probability percentage.
    /// </summary>
    public sealed class MalfunctionProbability : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MalfunctionProbability"/> class.
        /// </summary>
        /// <param name="value">The probability value.</param>
        private MalfunctionProbability(decimal value) => Value = value;

        /// <summary>
        /// Gets the probability value.
        /// </summary>
        public decimal Value { get; private set; }

        /// <summary>
        /// Creates a new <see cref="SpeedInKilometersPerHour"/> instance based on the specified value.
        /// </summary>
        /// <param name="probability">The probability value.</param>
        /// <returns>The result of the malfunction probability process containing the malfunction probability or an error.</returns>
        public static Result<MalfunctionProbability> Create(decimal probability) =>
            Result.Success(probability)
                .Ensure(x => x >= decimal.Zero && x <= 1.0m, DomainErrors.MalfunctionProbability.InvalidProbability)
                .Map(x => new MalfunctionProbability(x));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
