using System;
using System.Collections.Generic;
using Akka.Actor;

public class UserStatsCurrentStationActor : ReceiveActor
{
    public UserStatsCurrentStationActor()
    {
        _stats = new Dictionary<string, int?>();
        Receive<UserStationMessage>(m => OnReceivedUserStationMessage(m));
    }

    private readonly Dictionary<string, int?> _stats;

    private void OnReceivedUserStationMessage(UserStationMessage message)
    {
        CreateChildIfNotExists(message.UsersName);

        var previousStation = _stats[message.UsersName];
        _stats[message.UsersName] = message.StationNumber;

        if (previousStation != _stats[message.UsersName])
        {
            NotifyPreviousStationUserHasLeft(previousStation, message.UsersName);
            NotifyNewStationUserHasArrived(message.StationNumber, message.UsersName);
        }
    }

    private void NotifyNewStationUserHasArrived(int stationNumber, string usersName)
    {
        Context.ActorSelection($"/user/PickingActor/StationCoordinatorActor")
                   .Tell(new StationCoordinatorActor.UserArrivedMessage(usersName, stationNumber));
    }

    private void NotifyPreviousStationUserHasLeft(int? previousStation, string usersName)
    {
        if (previousStation.HasValue)
        {
            Context.ActorSelection($"/user/PickingActor/StationCoordinatorActor")
                        .Tell(new StationCoordinatorActor.UserLeftMessage(usersName, previousStation.Value));
        }
    }


    // Make sure we have stats for the current user.
    private void CreateChildIfNotExists(string usersName)
    {
        if (!_stats.ContainsKey(usersName))
        {
            _stats.Add(usersName, null);
        }
    }

    #region Messages

    public class UserStationMessage
    {
        public string UsersName { get; private set; }
        public int StationNumber { get; private set; }

        public UserStationMessage(string usersName, int stationNumber)
        {
            UsersName = usersName;
            StationNumber = stationNumber;
        }
    }

    #endregion
}