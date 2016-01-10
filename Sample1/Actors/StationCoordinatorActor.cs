using System;
using System.Collections.Generic;
using Akka.Actor;

public class StationCoordinatorActor : ReceiveActor
{

    // Constructor
    public StationCoordinatorActor()
    {
        _stations = new Dictionary<int, IActorRef>();

        // define what messages an actor will act upon
        Receive<StationUserSendMessage>(m =>
        {
            CreateChildIfNotExists(m.StationNumber);

            var stationRef = _stations[m.StationNumber];
            stationRef.Tell(m);
        });
    }

    private void CreateChildIfNotExists(int stationNumber)
    {
        if (_stations.ContainsKey(stationNumber))
            return;

        _stations.Add(stationNumber, Context.ActorOf(Props.Create(() => new StationActor(stationNumber)), $"Station{stationNumber}"));
    }

    private Dictionary<int, IActorRef> _stations;
}