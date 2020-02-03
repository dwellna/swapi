using System;
using System.Collections.Generic;
using System.Linq;

namespace StarshipResupplyCalculator.Logic.Parser
{
    public class ConsumableParser
    {
        /// <summary>
        /// A dictionary of parsing logic.
        /// </summary>
        private static Dictionary<string, Func<int, int>> _unitToHoursConverter = new Dictionary<string, Func<int, int>>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "hours", hours => hours },
            { "hour", hour => hour },
            { "days", days => days * 24 },
            { "day", day => day * 24 },
            { "weeks", weeks => weeks * 7 * 24 },
            { "week", week => week * 7 * 24 },
            { "months", months => months * 30 * 24 },
            { "month", month => month * 30 * 24 },
            { "years", years => years * 365 * 24 },
            { "year", year => year * 365 * 24 }
        };

        /// <summary>
        /// Parses the consumables of a starship.
        /// </summary>
        /// <param name="consumables">The consumable string, e.g. '6 months'.</param>
        /// <returns>The time span represantation of the given consumables.</returns>
        public static TimeSpan Parse(string consumables)
        {
            if (consumables.Equals("unknown", StringComparison.InvariantCultureIgnoreCase))
            {
                return TimeSpan.MinValue;
            }

            var stringParts = consumables.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var amount = int.Parse(stringParts.First());
            var unit = stringParts.Last();
            return new TimeSpan(0, _unitToHoursConverter[unit](amount), 0, 0);
        }
    }
}
