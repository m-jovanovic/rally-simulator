namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the number of vehicles by vehicle type.
    /// </summary>
    public class VehiclesByType
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of vehicles.
        /// </summary>
        public int Count { get; set; }
    }
}
