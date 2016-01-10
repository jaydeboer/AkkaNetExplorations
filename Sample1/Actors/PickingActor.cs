using Akka.Actor;

public class PickingActor : ReceiveActor
{
    public PickingActor()
    {
        _stationCoordinator = Context.ActorOf(Props.Create(() => new StationCoordinatorActor()), "StationCoordinatorActor");
        _userCoordinator = Context.ActorOf(Props.Create(() => new UserCoordinatorActor()), "UserCoordinatorActor");
        
        Receive<StationUserSendMessage>(message => OnStationUserSendMessage(message));
    }
    
    private readonly IActorRef _stationCoordinator;    
    private readonly IActorRef _userCoordinator;

    private void OnStationUserSendMessage(StationUserSendMessage message)
    {
        _userCoordinator.Forward(message);
    }
}