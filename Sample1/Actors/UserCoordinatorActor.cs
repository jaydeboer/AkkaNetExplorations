using System.Collections.Generic;
using Akka.Actor;

public class UserCoordinatorActor : ReceiveActor
{
    public UserCoordinatorActor()
    {
        _users = new Dictionary<string, IActorRef>();
        _statsCoordinator = Context.ActorOf(Props.Create(() => new UserStatsCoordinatorActor()), "UserStatsCoordinatorActor");
        
        Receive<StationUserSendMessage>(message => OnReceivedStationUserSendMessage(message));
    }

    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        CreateChildIfNotExists(message.UsersName);
        
        _statsCoordinator.Forward(message);
        _users[message.UsersName].Tell(new SendPerformedMessage(message.StationNumber));
    }

    private readonly Dictionary<string, IActorRef> _users;
    private readonly IActorRef _statsCoordinator;
    
    private void CreateChildIfNotExists(string usersName)
    {
        if (_users.ContainsKey(usersName))
            return;
        
        _users.Add(usersName, Context.ActorOf(Props.Create(() => new UserActor(usersName)), $"User{usersName}"));
    }

}