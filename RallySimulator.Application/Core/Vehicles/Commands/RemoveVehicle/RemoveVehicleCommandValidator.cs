using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;

namespace RallySimulator.Application.Core.Vehicles.Commands.RemoveVehicle
{
    /// <summary>
    /// Represents the <see cref="RemoveVehicleCommand"/> validator.
    /// </summary>
    public sealed class RemoveVehicleCommandValidator : AbstractValidator<RemoveVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveVehicleCommandValidator"/> class.
        /// </summary>
        public RemoveVehicleCommandValidator() =>
            RuleFor(x => x.VehicleId).GreaterThan(0).WithError(ValidationErrors.Vehicle.IdentifierIsRequired);
    }
}
