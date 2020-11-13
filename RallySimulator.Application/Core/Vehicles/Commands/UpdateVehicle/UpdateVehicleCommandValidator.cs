using System;
using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Commands.UpdateVehicle
{
    /// <summary>
    /// Represents the <see cref="UpdateVehicleCommand"/> validator.
    /// </summary>
    public sealed class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVehicleCommandValidator"/> class.
        /// </summary>
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.VehicleId).GreaterThan(0).WithError(ValidationErrors.Vehicle.IdentifierIsRequired);

            RuleFor(x => x.TeamName).NotEmpty().WithError(ValidationErrors.Vehicle.TeamNameIsRequired);

            RuleFor(x => x.ModelName).NotEmpty().WithError(ValidationErrors.Vehicle.ModelNameIsRequired);

            RuleFor(x => x.ManufacturingDate).NotEmpty().WithError(ValidationErrors.Vehicle.ManufacturingDateIsRequired);

            RuleFor(x => x.VehicleSubtype)
                .Must(x => Enum.IsDefined(typeof(VehicleSubtype), x))
                .WithError(ValidationErrors.Vehicle.SubtypeIsRequired);
        }
    }
}
