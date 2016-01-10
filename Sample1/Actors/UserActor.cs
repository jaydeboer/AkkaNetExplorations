using System;
using Akka.Actor;

public class UserActor : ReceiveActor
{
    public static IActorRef Create(ActorSystem system, string name)
    {
        return system.ActorOf(Props.Create(() => new UserActor(name)), name);
    }
    public int? CurrentStation { get; private set; }
    public string Name { get; private set; }

    public UserActor(string name)
    {
        Name = name;
        Receive<SendPerformedMessage>(message => HandleSendPerformed(message));
    }

    private void HandleSendPerformed(SendPerformedMessage message)
    {
        var previousStation = CurrentStation;
        if (!CurrentStation.HasValue || CurrentStation.Value != message.StationId)
        {
            if (previousStation.HasValue)
            {
                var oldStationActor = Context.ActorSelection($"/user/StationCoordinatorActor/Station{previousStation.Value}");
                oldStationActor.Tell(new StationUserLeftMessage(Name));
            }

            CurrentStation = message.StationId;

            Console.WriteLine($"Send performed by {Name} at station {CurrentStation.Value}");

            var selection = Context.ActorSelection($"/user/StationCoordinatorActor");
            selection.Tell(new StationUserSendMessage(message.StationId, Name));
        }
    }
}