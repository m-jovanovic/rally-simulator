namespace RallySimulator.Api.Constants
{
    /// <summary>
    /// Contains the API endpoint routes.
    /// </summary>
    internal static class ApiRoutes
    {
        /// <summary>
        /// Contains the races routes.
        /// </summary>
        internal static class Races
        {
            /// <summary>
            /// The create race route.
            /// </summary>
            internal const string CreateRace = "races";

            /// <summary>
            /// The start race route.
            /// </summary>
            internal const string StartRace = "races/{raceId:int}";

            /// <summary>
            /// The get race status route.
            /// </summary>
            internal const string GetRaceStatus = "races/{raceId:int}/status";

            /// <summary>
            /// The get race leaderboard route.
            /// </summary>
            internal const string GetRaceLeaderboard = "races/{raceId:int}/leaderboard";
        }

        /// <summary>
        /// Contains the vehicles routes.
        /// </summary>
        internal static class Vehicles
        {
            /// <summary>
            /// The create vehicle route.
            /// </summary>
            internal const string CreateVehicle = "vehicles";

            /// <summary>
            /// The start vehicle route.
            /// </summary>
            internal const string UpdateVehicle = "vehicles/{vehicleId:int}";

            /// <summary>
            /// The remove vehicle route.
            /// </summary>
            internal const string RemoveVehicle = "vehicles/{vehicleId:int}";

            /// <summary>
            /// The get vehicle route.
            /// </summary>
            internal const string GetVehicleStatistics = "vehicles/{vehicleId:int}/statistics";

            /// <summary>
            /// The get vehicle subtypes route.
            /// </summary>
            internal const string GetVehicleSubtypes = "vehicles/subtypes";
        }
    }
}
