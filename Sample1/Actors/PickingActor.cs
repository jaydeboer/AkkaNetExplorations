using Akka.Actor;

public class PickingActor : ReceiveActor
{
    public PickingActor()
    {
        Context.ActorOf(Props.Create(() => new StationCoordinatorActor()), "StationCoordinatorActor");
        Context.ActorOf(Props.Create(() => new UserCoordinatorActor()), "UserCoordinatorActor");
    }
}