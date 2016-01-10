using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

public class StationActor : ReceiveActor
{
    public int Id { get; private set; }

    public string[] CurrentUsers
    {
        get { return _currentUsers.ToArray(); }
    }

    // Constructor
    public StationActor(int id)
    {
        Id = id;
        _currentUsers = new HashSet<string>();

        Console.WriteLine($"we were station {id}");

        // define what messages an actor will act upon
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
        Receive<StationUserLeftMessage>(m => OnReceivedStationUserLeftMessage(m));
    }

    private readonly HashSet<string> _currentUsers;

    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        _currentUsers.Add(message.UsersName);
        Console.WriteLine($"The user {message.UsersName} has arrived at station {Id}");
    }

    private void OnReceivedStationUserLeftMessage(StationUserLeftMessage message)
    {
        _currentUsers.Remove(message.Name);
        Console.WriteLine($"The user {message.Name} has left station {Id}.");
    }
}