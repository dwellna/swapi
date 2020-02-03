using System.Linq;

namespace StarshipResupplyCalculator.Logic.Parser
{
    public static class MGLTParser
    {
        /// <summary>
        /// Parses the starship megalight value.
        /// </summary>
        /// <param name="mglt">The megalight value as string.</param>
        /// <returns>The megalight value as integar.</returns>
        public static int Parse(string mglt)
        {
            if (mglt.Equals("unknown", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return -1;
            }

            return int.Parse(mglt.Split(new[] { " " }, System.StringSplitOptions.RemoveEmptyEntries).First());
        }
    }
}
