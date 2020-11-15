namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the vehicle status enumeration.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// The pending status, before the race has begun.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The racing status.
        /// </summary>
        Racing = 2,

        /// <summary>
        /// The waiting for repair status. The vehicle has suffered a light malfunction.
        /// </summary>
        WaitingForRepair = 3,

        /// <summary>
        /// The broken status. The vehicle has suffered a heavy malfunction.
        /// </summary>
        Broken = 4,

        /// <summary>
        /// The completed race status. The vehicle has successfully completed the race.
        /// </summary>
        CompletedRace = 5
    }
}
