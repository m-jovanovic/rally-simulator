using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Core.Events;
using RallySimulator.Domain.Primitives.Events;

namespace RallySimulator.Application.Core.Races.Events.RaceStarted
{
    /// <summary>
    /// Represents the <see cref="RaceStartedDomainEvent"/> handler.
    /// </summary>
    internal sealed class RaceStartedDomainEventHandler : IDomainEventHandler<RaceStartedDomainEvent>
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaceStartedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        public RaceStartedDomainEventHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

        /// <inheritdoc />
        public async Task Handle(RaceStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            const string sql = "UPDATE Vehicle SET StartTimeUtc = @StartTimeUtc, Status = @Status WHERE RaceId = @RaceId";

            await _dbConnection.ExecuteAsync(
                sql,
                new
                {
                    notification.RaceId,
                    notification.StartTimeUtc,
                    Status = VehicleStatus.Racing
                });
        }
    }
}
