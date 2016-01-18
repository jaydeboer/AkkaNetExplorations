using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Sample.Services;
using Sample.Services.Models;
using Akka.DI.Core;

public class StationActor : ReceiveActor, IWithUnboundedStash
{

    // Constructor
    public StationActor(IStationService stationService)
    {
        _stationService = stationService;

        Console.WriteLine($"Creating a station actor");
        Unconfigured();
    }

    public IStash Stash { get; set; }
    private readonly IStationService _stationService;
    private Station _station;

    #region States of our actor
    private void Unconfigured()
    {
        Receive<Station>(m => OnReceivedAssignStationMessage(m));
        ReceiveAny(m => Stash.Stash());
    }

    private void Configured()
    {
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
        Receive<StationUserLeftMessage>(m => OnReceivedStationUserLeftMessage(m));
    }
    #endregion

    #region Messages
    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        _station.ActiveUsers.Add(message.UsersName);
        Console.WriteLine($"The user {message.UsersName} has arrived at station {_station.Id}");
    }

    private void OnReceivedStationUserLeftMessage(StationUserLeftMessage message)
    {
        _station.ActiveUsers.Remove(message.Name);
        Console.WriteLine($"The user {message.Name} has left station {_station.Id}");
    }

    /// <summary>
    /// A station actor should get a station assigned to it.
    /// </summary>
    private void OnReceivedAssignStationMessage(Station message)
    {
        // We're assuming no duplicates go on here.
        _station = message;
        _stationService.AddStation(_station);

        Console.WriteLine($"Assigning a station actor for station {message.Id}");
        Stash.UnstashAll();
        Become(Configured);
    }
    #endregion
}