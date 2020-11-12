using System.Collections.Generic;
using RallySimulator.Domain.Core.Errors;
using RallySimulator.Domain.Primitives;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the team name value object.
    /// </summary>
    public sealed class TeamName : ValueObject
    {
        /// <summary>
        /// The name maximum length.
        /// </summary>
        public const int MaxLength = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamName"/> class.
        /// </summary>
        /// <param name="value">The team name value.</param>
        private TeamName(string value) => Value = value;

        /// <summary>
        /// Gets the name value.
        /// </summary>
        public string Value { get; }

        public static implicit operator string(TeamName name) => name?.Value ?? string.Empty;

        /// <summary>
        /// Creates a new <see cref="TeamName"/> instance based on the specified value.
        /// </summary>
        /// <param name="name">The team name value.</param>
        /// <returns>The result of the team name creation process containing the team name or an error.</returns>
        public static Result<TeamName> Create(string name) =>
            Result.Create(name, DomainErrors.TeamName.NullOrEmpty)
                .Ensure(x => !string.IsNullOrWhiteSpace(x), DomainErrors.TeamName.NullOrEmpty)
                .Ensure(x => x.Length <= MaxLength, DomainErrors.TeamName.LongerThanAllowed)
                .Map(x => new TeamName(x));

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
