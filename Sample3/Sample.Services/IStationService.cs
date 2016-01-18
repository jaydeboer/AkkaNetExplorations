using System.Collections.Generic;
using Sample.Services.Models;

namespace Sample.Services
{

    public interface IStationService
    {
        IList<Station> GetStations();

        void AddStation(Station station);
    }
}