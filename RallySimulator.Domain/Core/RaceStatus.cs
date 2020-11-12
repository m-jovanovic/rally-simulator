namespace RallySimulator.Domain.Core
{
    /// <summary>
    /// Represents the race status enumeration.
    /// </summary>
    public enum RaceStatus
    {
        /// <summary>
        /// The pending status. The race has not yet started.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The running status. The race has started.
        /// </summary>
        Running = 1,

        /// <summary>
        /// The finished status. The race has completed, all of the vehicle have finished the race or have broken down.
        /// </summary>
        Finished = 2
    }
}
