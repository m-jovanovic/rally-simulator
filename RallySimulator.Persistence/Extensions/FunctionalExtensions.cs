using System;
using System.Collections.Generic;

namespace RallySimulator.Persistence.Extensions
{
    /// <summary>
    /// Contains general functional extensions methods.
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Performs the specified action with each element of the sequence.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="source">The enumerable source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T obj in source)
            {
                action(obj);
            }
        }
    }
}
