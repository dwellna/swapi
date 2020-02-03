using Moq;
using System;
using Xunit;
using StarshipResupplyCalculator.Contracts;
using System.Threading.Tasks;
using System.Collections.Generic;
using StarshipResupplyCalculator.Logic;
using System.Linq;

namespace StarshipResupplyCalculator.Tests
{
    public class ResupplyCalculatorTests
    {
        [Fact]
        public async Task CalculateStops()
        {
            var client = new Mock<ISwApiClient>();
            client.Setup(c => c.GetStarships()).Returns(GetTestSharships());
            var calculator = new Calculator(client.Object);
            var stops = await calculator.CalculateStops(1000000).ToArrayAsync();
            Assert.Equal(3, stops.Length);
            Assert.Equal(9, stops[0].Stops);
            Assert.Equal(74, stops[1].Stops);
            Assert.Equal(11, stops[2].Stops);
        }

        private async IAsyncEnumerable<Starship> GetTestSharships()
        {
            yield return new Starship
            {
                Consumables = "2 months",
                MGLT = "75",
                Name = "Millennium Falcon"
            };

            yield return new Starship
            {
                Consumables = "1 week",
                MGLT = "80",
                Name = "Y-Wing"
            };

            yield return new Starship
            {
                Consumables = "6 months",
                MGLT = "20",
                Name = "Rebel transport"
            };

            yield return new Starship
            {
                Consumables = "2 days",
                MGLT = "unknown",
                Name = "Republican Cruiser"
            };

            yield return new Starship
            {
                Consumables = "unknown",
                MGLT = "50000",
                Name = "Republican Fighter"
            };

            await Task.CompletedTask;
        }
    }
}
