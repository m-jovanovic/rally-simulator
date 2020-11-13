using System;
using RallySimulator.Application.Abstractions.Common;

namespace RallySimulator.Infrastructure.Common
{
    /// <summary>
    /// Represents the current machine date and time.
    /// </summary>
    internal sealed class MachineDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
