using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarshipResupplyCalculator.Contracts
{
    /// <summary>
    /// This interface partially defines a consumer of SW api. 
    /// </summary>
    public interface ISwApiClient
    {
        IAsyncEnumerable<Starship> GetStarships();

        Task<Starship> GetStarship(int id);
    }
}
