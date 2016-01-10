using System;
using Akka.Actor;

// Handles dispaching of messages to record statistics based on user events.
public class UserStatsCoordinatorActor : ReceiveActor
{
    public UserStatsCoordinatorActor()
    {
        _sendCountActor = Context.ActorOf(Props.Create(() => new UserStatsSendCountActor()), "SendCountActor");
        _currentStationActor = Context.ActorOf(Props.Create(() => new UserStatsCurrentStationActor()), "CurrentStationActor");
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
    }
    
    private readonly IActorRef _sendCountActor;
    private readonly IActorRef _currentStationActor;
    
    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        Console.WriteLine("Coordinating user stats.");
        _sendCountActor.Tell(new UserStatsSendCountActor.SendCompletedMessage(message.UsersName));
        _currentStationActor.Tell(new UserStatsCurrentStationActor.UserStationMessage(message.UsersName, message.StationNumber));
    }
    
}