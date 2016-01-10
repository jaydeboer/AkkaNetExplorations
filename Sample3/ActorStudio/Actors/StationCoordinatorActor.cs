using System;
using System.Collections.Generic;
using Akka.Actor;

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
            _stationActors.Add(stationNumber, Context.ActorOf(Props.Create(() => new StationActor(stationNumber)), $"Station{stationNumber}"));

        return _stationActors[stationNumber];
    }

    private Dictionary<int, IActorRef> _stationActors;
}