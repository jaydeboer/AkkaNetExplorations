using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

public class StationActor : ReceiveActor
{   
    public int Id { get; private set; }
    
    public string[] CurrentUsers{
        get { return _currentUsers.ToArray(); }
    }
    
    public StationActor(int id)
    {
        Id = id;
        Console.WriteLine("we were here");
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
        Receive<StationUserLeftMessage>(m => OnRecievedStationUserLeftMessage(m));
        _currentUsers = new HashSet<string>();
    }
    
    private readonly HashSet<string> _currentUsers;
    
    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        _currentUsers.Add(message.UsersName);
        Console.WriteLine($"The user {message.UsersName} has arrived at station {Id}");
    }
    
    private void OnRecievedStationUserLeftMessage(StationUserLeftMessage message)
    {
        _currentUsers.Remove(message.Name);
        Console.WriteLine($"The user {message.Name} has left station {Id}.");
    }
}