using System.Threading.Tasks;

namespace RallySimulator.Domain.Services
{
    /// <summary>
    /// Represents the running race checker interface.
    /// </summary>
    public interface IRunningRaceChecker
    {
        /// <summary>
        /// Checks if there is a race that is already running.
        /// </summary>
        /// <returns>True if there is a race that is already running, otherwise false.</returns>
        Task<bool> IsAnyRaceRunning();
    }
}
