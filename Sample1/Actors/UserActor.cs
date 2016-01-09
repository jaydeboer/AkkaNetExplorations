using System;
using Akka.Actor;

public class UserActor : ReceiveActor
{
    public int? CurrentStation { get; private set; }
    public string Name {get; private set;}
    
    public UserActor(string name)
    {
        Name = name;
        Receive<SendPerformedMessage>(message => HandleSendPerformed(message));
    }

    private void HandleSendPerformed(SendPerformedMessage message)
    {
        CurrentStation = message.StationId;
        Console.WriteLine($"Send performed by {Name} at station {CurrentStation.Value}");
        var selection = Context.ActorSelection($"/user/{message.StationId}");
        Console.WriteLine(selection.PathString);
        selection.Tell(new StationUserSendMessage(Name));
    }
}