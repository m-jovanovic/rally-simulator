using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicles
{
    /// <summary>
    /// Represents the <see cref="GetVehiclesQuery"/> validator.
    /// </summary>
    public sealed class GetVehiclesQueryValidator : AbstractValidator<GetVehiclesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesQueryValidator"/> class.
        /// </summary>
        public GetVehiclesQueryValidator()
        {
            RuleFor(x => x.ManufacturingDateTo)
                .GreaterThanOrEqualTo(x => x.ManufacturingDateFrom)
                .When(x => x.ManufacturingDateFrom.HasValue && x.ManufacturingDateTo.HasValue)
                .WithError(ValidationErrors.Vehicle.ManufacturingDateToPrecedesDateFrom);

            RuleFor(x => x.DistanceTo)
                .GreaterThanOrEqualTo(x => x.DistanceFrom)
                .When(x => x.DistanceFrom.HasValue && x.DistanceTo.HasValue)
                .WithError(ValidationErrors.Vehicle.DistanceToIsLessThanDistanceFrom);
        }
    }
}
