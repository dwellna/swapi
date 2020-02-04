using StarshipResupplyCalculator.Contracts;
using StarshipResupplyCalculator.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StarshipResupplyCalculator.Tests
{
    public class ResupplyCalculatorTests
    {
        [Fact]
        public async Task GivenMilleniumFalconExpect9Stops()
        {
            var calculator = new Calculator();
            var shipAndStops = await calculator.CalculateStops(GetMilleniumFalcon(), 1000000).FirstAsync();
            Assert.Equal(9, shipAndStops.Stops);
        }

        [Fact]
        public async Task GivenRebelTransportExpect11Stops()
        {
            var calculator = new Calculator();
            var shipAndStops = await calculator.CalculateStops(GetRebelTransport(), 1000000).FirstAsync();
            Assert.Equal(11, shipAndStops.Stops);
        }

        [Fact]
        public async Task GivenYWingExpect74Stops()
        {
            var calculator = new Calculator();
            var shipAndStops = await calculator.CalculateStops(GetYWing(), 1000000).FirstAsync();
            Assert.Equal(74, shipAndStops.Stops);
        }

        [Fact]
        public async Task GivenRepublicanCruiserExpectUnknownStops()
        {
            var calculator = new Calculator();
            var shipAndStops = await calculator.CalculateStops(GetRepublicanCruiser(), 1000000).FirstAsync();
            Assert.Equal(-1, shipAndStops.Stops);
        }

        [Fact]
        public async Task GivenRepublicanFighterExpectUnknownStops()
        {
            var calculator = new Calculator();
            var shipAndStops = await calculator.CalculateStops(GetRepublicanFighter(), 1000000).FirstAsync();
            Assert.Equal(-1, shipAndStops.Stops);
        }


        private async IAsyncEnumerable<Starship> GetMilleniumFalcon()
        {
            yield return new Starship
            {
                Consumables = "2 months",
                MGLT = "75",
                Name = "Millennium Falcon"
            };

            await Task.CompletedTask;
        }

        private async IAsyncEnumerable<Starship> GetRebelTransport()
        {
            yield return new Starship
            {
                Consumables = "6 months",
                MGLT = "20",
                Name = "Rebel Transport"
            };

            await Task.CompletedTask;
        }

        private async IAsyncEnumerable<Starship> GetYWing()
        {
            yield return new Starship
            {
                Consumables = "1 week",
                MGLT = "80",
                Name = "Y-Wing"
            };

            await Task.CompletedTask;
        }

        private async IAsyncEnumerable<Starship> GetRepublicanCruiser()
        {
            yield return new Starship
            {
                Consumables = "2 days",
                MGLT = "unknown",
                Name = "Republican Cruiser"
            };

            await Task.CompletedTask;
        }

        private async IAsyncEnumerable<Starship> GetRepublicanFighter()
        {
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
