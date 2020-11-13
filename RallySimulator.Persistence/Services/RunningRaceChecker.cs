using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Services;

namespace RallySimulator.Persistence.Services
{
    /// <summary>
    /// Represents the running race checker.
    /// </summary>
    internal sealed class RunningRaceChecker : IRunningRaceChecker
    {
        private readonly IDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningRaceChecker"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RunningRaceChecker(IDbContext dbContext) => _dbContext = dbContext;

        /// <inheritdoc />
        public async Task<bool> IsAnyRaceRunning() => await _dbContext.Set<Race>().AnyAsync(x => x.Status == RaceStatus.Running);
    }
}
