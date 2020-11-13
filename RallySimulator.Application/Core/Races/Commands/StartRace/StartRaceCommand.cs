using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Races.Commands.StartRace
{
    /// <summary>
    /// Represents the command for starting a race.
    /// </summary>
    public sealed class StartRaceCommand : ICommand<Result>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartRaceCommand"/> class.
        /// </summary>
        /// <param name="raceId">The race identifier.</param>
        public StartRaceCommand(int raceId) => RaceId = raceId;

        /// <summary>
        /// Gets the race identifier.
        /// </summary>
        public int RaceId { get; }
    }
}
