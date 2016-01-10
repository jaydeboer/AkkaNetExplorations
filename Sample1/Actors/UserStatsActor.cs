using System.Collections.Generic;
using Akka.Actor;

public class UserStatsActor : ReceiveActor
{
    public UserStatsActor()
    {
        _stats = new Dictionary<string, UserStats>();
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
    }
    
    private readonly Dictionary<string, UserStats> _stats;
    
    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        var stats = GetStatsForUser(message.UsersName);
        stats.SendCount++;
        
        if (stats.CurrentStationId != message.StationNumber)
        {
            if (stats.CurrentStationId != 0)
                Context.ActorSelection($"/user/PickingActor/StationCoordinatorActor/Station{stats.CurrentStationId}")
                    .Tell(new StationUserLeftMessage(message.UsersName));
                    
            stats.CurrentStationId = message.StationNumber;
        }
    }
    
    // This will make sure the user is in the dictionary and return it
    private UserStats GetStatsForUser(string usersName)
    {
        if (!_stats.ContainsKey(usersName))
        {
            _stats.Add(usersName, new UserStats());
        }
        return _stats[usersName];
    }
    #region Messages
    
    #endregion
}