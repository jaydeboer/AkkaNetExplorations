using Xunit;
using Sample.Services.Impl;
using Sample.Services.Models;

namespace Sample.Service.Tests
{
    /// <summary>
    /// Testing the Sample Station Service
    /// <see cref="http://xunit.github.io/docs/getting-started-dnx.html"/>
    /// </summary>
    public class SampleStationServiceTests
    {
        [Fact]
        public void NoStationsToStart()
        {
            Assert.Equal(new SampleStationService().GetStations().Count, 0);
        }

        [Fact]
        public void AddingStationShouldExist()
        {
            var service = new SampleStationService();
            service.AddStation(new Station());

            Assert.Equal(service.GetStations().Count, 1);
        }
    }
}