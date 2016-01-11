using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.DI.Core;
using Sample.Services.Models;

/// <summary>
/// A Station Coordinator manages its collection of children (station actors).
/// </summary>
public class StationCoordinatorActor : ReceiveActor
{

    // Constructor
    public StationCoordinatorActor()
    {
        _stationActors = new Dictionary<int, IActorRef>();

        // define what messages an actor will act upon
        Receive<StationUserSendMessage>(m =>
        {
            var stationRef = CreateChildIfNotExists(m.StationNumber);
            stationRef.Tell(m);
        });
    }

    private IActorRef CreateChildIfNotExists(int stationNumber)
    {
        if (!_stationActors.ContainsKey(stationNumber))
        {
            var stn = Context.ActorOf(Context.DI().Props<StationActor>(), $"Station{stationNumber}");
            _stationActors.Add(stationNumber, stn);

            // Configure this station
            stn.Tell(new StationUserSendMessage(stationNumber, "One"), stn);
            stn.Tell(new StationUserSendMessage(stationNumber, "Two"), stn);
            stn.Tell(new Station() { Id = stationNumber, Name = $"Station {stationNumber}" });
            stn.Tell(new StationUserSendMessage(stationNumber, "Three"), stn);
            stn.Tell(new StationUserSendMessage(stationNumber, "Four"), stn);

        }

        return _stationActors[stationNumber];
    }

    private Dictionary<int, IActorRef> _stationActors;
}