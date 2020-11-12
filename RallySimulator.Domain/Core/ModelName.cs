using System.Collections.Generic;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the model name value object.
    /// </summary>
    public sealed class ModelName : ValueObject
    {
        /// <summary>
        /// The name maximum length.
        /// </summary>
        public const int MaxLength = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelName"/> class.
        /// </summary>
        /// <param name="value">The model name value.</param>
        private ModelName(string value) => Value = value;

        /// <summary>
        /// Gets the name value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(ModelName name) => name?.Value ?? string.Empty;

        /// <summary>
        /// Creates a new <see cref="ModelName"/> instance based on the specified value.
        /// </summary>
        /// <param name="name">The model name value.</param>
        /// <returns>The result of the model name creation process containing the model name or an error.</returns>
        public static Result<ModelName> Create(string name) =>
            Result.Create(name, DomainErrors.ModelName.NullOrEmpty)
                .Ensure(x => !string.IsNullOrWhiteSpace(x), DomainErrors.ModelName.NullOrEmpty)
                .Ensure(x => x.Length <= MaxLength, DomainErrors.ModelName.LongerThanAllowed)
                .Map(x => new ModelName(x));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
