namespace RallySimulator.Application.Contracts.Vehicles
{
    /// <summary>
    /// Represents the vehicle type response.
    /// </summary>
    public sealed class VehicleTypeResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
