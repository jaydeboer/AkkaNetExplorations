using System.Collections.Generic;
using Akka.Actor;

public class UserCoordinatorActor : ReceiveActor
{
    public UserCoordinatorActor()
    {
        _users = new Dictionary<string, IActorRef>();
        
    }
    
    private readonly Dictionary<string, IActorRef> _users;
    
    private void CreateChildIfNotExists(string usersName)
    {
        if (_users.ContainsKey(usersName))
            return;
        
        _users.Add(usersName, Context.ActorOf(Props.Create(() => new UserActor(usersName)), $"User{usersName}"));
    }

}