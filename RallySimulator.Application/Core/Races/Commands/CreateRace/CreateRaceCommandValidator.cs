using FluentValidation;
using RallySimulator.Application.Abstractions.Common;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;

namespace RallySimulator.Application.Core.Races.Commands.CreateRace
{
    /// <summary>
    /// Represents the <see cref="CreateRaceCommand"/> validator.
    /// </summary>
    public sealed class CreateRaceCommandValidator : AbstractValidator<CreateRaceCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRaceCommandValidator"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        public CreateRaceCommandValidator(IDateTime dateTime)
        {
            RuleFor(x => x.Year).GreaterThanOrEqualTo(dateTime.UtcNow.Year).WithError(ValidationErrors.Race.YearInThePast);

            RuleFor(x => x.Length).GreaterThanOrEqualTo(0).WithError(ValidationErrors.Race.NegativeLength);
        }
    }
}
