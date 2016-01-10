using Akka.Actor;

public class UserActor : ReceiveActor
{
    public static IActorRef Create(ActorSystem system, string name)
    {
        return system.ActorOf(Props.Create(() => new UserActor(name)), name);
    }
    public string Name { get; private set; }

    // Constructor
    public UserActor(string name)
    {
        Name = name;

        // define what messages an actor will act upon
        Receive<SendPerformedMessage>(message => OnReceivedSendPerformedMessage(message));
    }

    private void OnReceivedSendPerformedMessage(SendPerformedMessage message)
    {
        var selection = Context.ActorSelection($"/user/PickingActor/StationCoordinatorActor");
        selection.Tell(new StationUserSendMessage(message.StationId, Name));
    }
}