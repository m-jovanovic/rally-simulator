using System;
using System.Collections.Generic;
using System.Text;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Application.Contracts.Common;
using RallySimulator.Application.Contracts.Vehicles;
using RallySimulator.Domain.Core;

namespace RallySimulator.Application.Core.Vehicles.Queries.GetVehicles
{
    /// <summary>
    /// Represents the query for getting a collection of vehicles.
    /// </summary>
    public sealed class GetVehiclesQuery : IQuery<PagedList<VehicleResponse>>
    {
        private const int MaxPageSize = 50;
        private const string DefaultOrderBy = $"{nameof(Vehicle.TeamName)}.{nameof(Vehicle.TeamName.Value)}";
        private static readonly Dictionary<string, string> ValidOrderByColumnsDictionary = new()
        {
            { nameof(Vehicle.TeamName).ToLower(), $"{nameof(Vehicle.TeamName)}.{nameof(Vehicle.TeamName.Value)}" },
            { nameof(Vehicle.ModelName).ToLower(), $"{nameof(Vehicle.ModelName)}.{nameof(Vehicle.ModelName.Value)}" },
            { nameof(Vehicle.ManufacturingDate).ToLower(), nameof(Vehicle.ManufacturingDate) },
            { nameof(Vehicle.Status).ToLower(), nameof(Vehicle.Status) },
            { nameof(Vehicle.Distance).ToLower(), $"{nameof(Vehicle.Distance)}.{nameof(Vehicle.Distance.Value)}" }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesQuery"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        /// <param name="team">The team.</param>
        /// <param name="model">The model.</param>
        /// <param name="manufacturingDateFrom">The manufacturing date from.</param>
        /// <param name="manufacturingDateTo">The manufacturing date to.</param>
        /// <param name="status">The status.</param>
        /// <param name="distanceFrom">The distance from.</param>
        /// <param name="distanceTo">The distance to.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="orderBy">The order by.</param>
        public GetVehiclesQuery(
            int raceId,
            string team,
            string model,
            DateTime? manufacturingDateFrom,
            DateTime? manufacturingDateTo,
            int status,
            decimal? distanceFrom,
            decimal? distanceTo,
            int page,
            int pageSize,
            string orderBy)
        {
            RaceId = raceId;
            Team = team?.ToLower();
            Model = model?.ToLower();
            ManufacturingDateFrom = manufacturingDateFrom;
            ManufacturingDateTo = manufacturingDateTo;
            Status = status;
            FilterStatus = Enum.IsDefined(typeof(VehicleStatus), status);
            DistanceFrom = distanceFrom;
            DistanceTo = distanceTo;
            Page = page < 0 ? 1 : page;
            PageSize = pageSize < 0 ? 0 : pageSize > MaxPageSize ? MaxPageSize : pageSize;
            OrderBy = ValidateOrderBy(orderBy);
        }

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }

        /// <summary>
        /// Gets the team.
        /// </summary>
        public string Team { get; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the manufacturing date from.
        /// </summary>
        public DateTime? ManufacturingDateFrom { get; }

        /// <summary>
        /// Gets the manufacturing date to.
        /// </summary>
        public DateTime? ManufacturingDateTo { get; }

        /// <summary>
        /// Gets that status.
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// Gets a value indicating whether or not the status filter should be applied.
        /// </summary>
        public bool FilterStatus { get; }

        /// <summary>
        /// Gets the distance from.
        /// </summary>
        public decimal? DistanceFrom { get; }

        /// <summary>
        /// Gets the distance to.
        /// </summary>
        public decimal? DistanceTo { get; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Validates the provided order by string.
        /// </summary>
        /// <param name="orderBy">The order by string.</param>
        /// <returns>The validated order by string or a default order by.</returns>
        private static string ValidateOrderBy(string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return DefaultOrderBy;
            }

            string[] orderByParts = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var orderByStringBuilder = new StringBuilder();

            foreach (string orderByPart in orderByParts)
            {
                string columnName = orderByPart.Trim(' ').Split(' ')[0].ToLower();

                if (!ValidOrderByColumnsDictionary.TryGetValue(columnName, out string orderByColumn))
                {
                    continue;
                }

                string orderByDirection = orderByPart.EndsWith(" desc", StringComparison.OrdinalIgnoreCase) ? " desc" : string.Empty;
                string orderByValue = $"{orderByColumn}{orderByDirection}, ";
                orderByStringBuilder.Append(orderByValue);
            }

            string validatedOrderBy = orderByStringBuilder.ToString().TrimEnd(',', ' ');

            return string.IsNullOrEmpty(validatedOrderBy) ? DefaultOrderBy : validatedOrderBy;
        }
    }
}
