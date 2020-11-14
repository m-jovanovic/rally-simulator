using System;
using FluentValidation;
using RallySimulator.Application.Extensions;
using RallySimulator.Application.Validation;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Races.Queries.GetRaceLeaderboardForVehicleType
{
    /// <summary>
    /// Represents the <see cref="GetRaceLeaderboardForVehicleTypeQuery"/> validator.
    /// </summary>
    public sealed class GetRaceLeaderBoardForVehicleTypeQueryValidator : AbstractValidator<GetRaceLeaderboardForVehicleTypeQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRaceLeaderBoardForVehicleTypeQueryValidator"/> class.
        /// </summary>
        public GetRaceLeaderBoardForVehicleTypeQueryValidator() =>
            RuleFor(x => x.VehicleType).Must(x => Enum.IsDefined(typeof(VehicleType), x)).WithError(ValidationErrors.VehicleType.NotValid);
    }
}
