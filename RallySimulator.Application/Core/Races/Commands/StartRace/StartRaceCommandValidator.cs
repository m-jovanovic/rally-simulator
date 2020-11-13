using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;

namespace RallySimulator.Application.Core.Races.Commands.StartRace
{
    /// <summary>
    /// Represents the <see cref="StartRaceCommand"/> validator.
    /// </summary>
    public sealed class StartRaceCommandValidator : AbstractValidator<StartRaceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartRaceCommandValidator"/> class.
        /// </summary>
        public StartRaceCommandValidator() => RuleFor(x => x.RaceId).GreaterThan(0).WithError(ValidationErrors.Race.IdentifierIsRequired);
    }
}
