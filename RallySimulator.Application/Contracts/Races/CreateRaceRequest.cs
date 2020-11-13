namespace RallySimulator.Application.Contracts.Races
{
    /// <summary>
    /// Represents the request for creating a race.
    /// </summary>
    public sealed class CreateRaceRequest
    {
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public int Year { get; set; }
    }
}
