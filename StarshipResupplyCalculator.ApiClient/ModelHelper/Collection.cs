using Newtonsoft.Json;
using System.Collections.Generic;

namespace StarshipResupplyCalculator.ApiClient.ModelHelper
{
    /// <summary>
    /// A collection object defined by the SW api.
    /// </summary>
    /// <typeparam name="T">The object type of the collection items.</typeparam>
    public class Collection<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
    }
}
