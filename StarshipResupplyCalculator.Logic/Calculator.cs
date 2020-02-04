using StarshipResupplyCalculator.Contracts;
using StarshipResupplyCalculator.Logic.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarshipResupplyCalculator.Logic
{
    /// <summary>
    /// This class calculates the amount of stops all starships avaliable on swapi will need to cover a certain distance.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Calculates the stops all on swapi available starshops need to cover the given distance.
        /// </summary>
        /// <param name="ships">The collection of starships to calculate the stops for.</param>
        /// <param name="distance">The distance to be covered.</param>
        /// <returns>A tuple containing the starship and the amount of stops necessary to cover the given distance.</returns>
        public IAsyncEnumerable<(Starship Ship, int Stops)> CalculateStops(IAsyncEnumerable<Starship> ships, int distance)
        {
            return ships.SelectAwait(ship => 
                new ValueTask<(Starship, int)>(Task.FromResult(
                    (ship, Stops: GetStops(MGLTParser.Parse(ship.MGLT), ConsumableParser.Parse(ship.Consumables), distance)))));
        }

        /// <summary>
        /// Calculates the stops necessary for the given parameters.
        /// </summary>
        /// <param name="mglt">The megalight value of a starship.</param>
        /// <param name="consumables">The time the consumables of that ship will last ubtil a stop is needed.</param>
        /// <param name="distance">The distance to be covered.</param>
        /// <returns>The amount of stops necessary.</returns>
        private int GetStops(int mglt, TimeSpan consumables, int distance)
        {
            if (mglt < 0 || consumables == TimeSpan.MinValue)
            {
                return -1;
            }

            var hours = (double)distance / (double)mglt;
            return (int)Math.Floor(hours / consumables.TotalHours);
        }
    }
}
