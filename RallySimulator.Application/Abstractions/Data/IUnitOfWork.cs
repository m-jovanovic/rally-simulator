using System.Threading;
using System.Threading.Tasks;

namespace RallySimulator.Application.Abstractions.Data
{
    /// <summary>
    /// Represents the unit of work interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all of the pending changes in the unit of work.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of entities that have been saved.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
