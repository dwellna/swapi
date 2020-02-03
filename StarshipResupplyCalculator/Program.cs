using StarshipResupplyCalculator.ApiClient;
using StarshipResupplyCalculator.Logic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StarshipResupplyCalculator
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (!args.Any())
            {
                throw new ApplicationException("Missing distance argument! Please provide a distance for the starthips to travel.");
            }

            if (!int.TryParse(args.First(), out var distance))
            {
                throw new ApplicationException("Cannot parse distance argument. Distance needs to be an integer.");
            }

            var client = new SwApiClient();
            var calculator = new Calculator(client);
            var shipsAndStops = calculator.CalculateStops(distance);
            await foreach(var (Ship, Stops) in shipsAndStops)
            {
                Console.WriteLine($"{Ship.Name}: {GetStopsString(Stops)}");
            }
        }

        /// <summary>
        /// Retrieves the string represenatsion of the number of stops.
        /// </summary>
        /// <param name="stops">The amount of stops to convert into a string.</param>
        /// <returns>The string representation of any positive integer, "unknown" otherwise.</returns>
        private static string GetStopsString(int stops)
        {
            if (stops < 0)
            {
                return "unknown";
            }

            return stops.ToString();
        }
    }
}
