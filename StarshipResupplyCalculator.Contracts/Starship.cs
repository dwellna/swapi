using Newtonsoft.Json;

namespace StarshipResupplyCalculator.Contracts
{
    /// <summary>
    /// This is a partial implementation of the Starthip object from swapi.co.
    /// It can be extended for future use cases.
    /// </summary>
    public class Starship
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("MGLT")]
        public string MGLT { get; set; }

        [JsonProperty("consumables")]
        public string Consumables { get; set; }
    }
}
