using System.Collections.Generic;
using Sample.Services.Models;

namespace Sample.Services.Impl
{

    public class SampleStationService : IStationService
    {
        public void AddStation(Station station)
        {
            if (!_stations.ContainsKey(station.Id))
            {
                _stations[station.Id] = station;
            }
        }

        public IList<Station> GetStations()
        {
            return new List<Station>(_stations.Values);
        }

        // Constructor
        public SampleStationService()
        {
            _stations = new Dictionary<int, Station>();
        }

        private IDictionary<int, Station> _stations;
    }
}