namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the number of vehicles by vehicle status.
    /// </summary>
    public class VehiclesByStatus
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the number of vehicles.
        /// </summary>
        public int Count { get; set; }
    }
}
