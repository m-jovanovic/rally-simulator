using System.Threading;
using System.Threading.Tasks;
using RallySimulator.Application.Abstractions.Data;
using RallySimulator.Application.Abstractions.Messaging;
using RallySimulator.Domain.Core;
using RallySimulator.Domain.Primitives.Result;

namespace RallySimulator.Application.Core.Races.Commands.CreateRace
{
    /// <summary>
    /// Represents the <see cref="CreateRaceCommand"/> handler.
    /// </summary>
    internal sealed class CreateRaceCommandHandler : ICommandHandler<CreateRaceCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRaceCommandHandler"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CreateRaceCommandHandler(IDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(CreateRaceCommand request, CancellationToken cancellationToken)
        {
            Result<LengthInKilometers> lengthResult = LengthInKilometers.Create(request.Length);

            if (lengthResult.IsFailure)
            {
                return Result.Failure(lengthResult.Error);
            }

            var race = new Race(request.Year, lengthResult.Value);

            _dbContext.Insert(race);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
