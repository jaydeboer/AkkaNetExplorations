using System;
using Akka.Actor;

public class StationActor : ReceiveActor
{
    public int Id { get; private set; }
    public StationActor(int id)
    {
        Id = id;
        Console.WriteLine("we were here");
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
    }
    
    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        Console.WriteLine($"The user {message.UsersName} has arrived at station {Id}");
    }
}