using System;
using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Commands.CreateVehicle
{
    /// <summary>
    /// Represents the <see cref="CreateVehicleCommand"/> validator.
    /// </summary>
    public sealed class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleCommandValidator"/> class.
        /// </summary>
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.RaceId).GreaterThan(0).WithError(ValidationErrors.Race.IdentifierIsRequired);

            RuleFor(x => x.TeamName).NotEmpty().WithError(ValidationErrors.Vehicle.TeamNameIsRequired);

            RuleFor(x => x.ModelName).NotEmpty().WithError(ValidationErrors.Vehicle.ModelNameIsRequired);

            RuleFor(x => x.ManufacturingDate).NotEmpty().WithError(ValidationErrors.Vehicle.ManufacturingDateIsRequired);

            RuleFor(x => x.VehicleSubtype)
                .Must(x => Enum.IsDefined(typeof(VehicleSubtype), x))
                .WithError(ValidationErrors.Vehicle.SubtypeIsRequired);
        }
    }
}
