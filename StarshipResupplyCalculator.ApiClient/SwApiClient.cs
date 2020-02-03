using Newtonsoft.Json;
using StarshipResupplyCalculator.ApiClient.ModelHelper;
using StarshipResupplyCalculator.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StarshipResupplyCalculator.ApiClient
{
    /// <summary>
    /// (Partly )Implements a client consuming the SW api.
    /// </summary>
    public class SwApiClient : ISwApiClient, IDisposable
    {
        private const string BaseAddress = @"http://swapi.co/api/";
        private const string AcceptHeader = "application/json";
        private HttpClient _client;

        public SwApiClient(string baseUrl = BaseAddress)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AcceptHeader));
        }

        /// <summary>
        /// Gets the starship with the given id.
        /// </summary>
        /// <param name="id">The id of the starthip to retrieve.</param>
        /// <returns>The requested starship.</returns>
        public async Task<Starship> GetStarship(int id)
        {
            return await GetAsync<Starship>($"starship/{id}");
        }

        /// <summary>
        /// Gets all available starships from the SW api.
        /// </summary>
        /// <returns>An asynchronous enumerable of starships.</returns>
        public async IAsyncEnumerable<Starship> GetStarships()
        {
            var page = 1;
            while (page > 0)
            {
                var ships = await GetAsync<StarshipCollection>($"starships?page={page++}");
                if (string.IsNullOrWhiteSpace(ships.Next))
                {
                    page = -1;
                }

                foreach (var ship in ships.Results)
                {
                    yield return ship;
                }
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Dispose();
        }

        /// <summary>
        /// Helper method to get an object of type T from the SW api.
        /// </summary>
        /// <typeparam name="T">Any object type supported by the SW api.</typeparam>
        /// <param name="url">The url used to retrieve the requested object.</param>
        /// <returns>The requested object. (asynchronously)</returns>
        private async Task<T> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
