using System.Collections.Generic;
using Akka.Actor;

/// <summary>
/// A Station Coordinator manages its collection of children (station actors).
/// </summary>
public class StationCoordinatorActor : ReceiveActor
{

    // Constructor
    public StationCoordinatorActor()
    {
        _stationActors = new Dictionary<int, IActorRef>();

        // define what messages an actor will act upon
        Receive<StationUserSendMessage>(message => OnReceivedStationUserSendMessage(message));
        Receive<StationListUsersRequestMessage>(message => OnReceivedStationListUsersRequestMessage(message));
        Receive<UserArrivedMessage>(message => OnReceivedUserArrivedMessage(message));
        Receive<UserLeftMessage>(message => OnReceivedUserLeftMessage(message));

    }

    private void OnReceivedStationListUsersRequestMessage(StationListUsersRequestMessage message)
    {
        CreateChildIfNotExists(message.StationId);
        _stationActors[message.StationId].Forward(message);
    }

    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        CreateChildIfNotExists(message.StationNumber);
        _stationActors[message.StationNumber].Tell(message);
    }

    private void OnReceivedUserLeftMessage(UserLeftMessage message)
    {
        CreateChildIfNotExists(message.StationNumber);
        _stationActors[message.StationNumber].Tell(new StationActor.UserLeftMessage(message.UsersName));
    }

    private void OnReceivedUserArrivedMessage(UserArrivedMessage message)
    {
        CreateChildIfNotExists(message.StationNumber);
        _stationActors[message.StationNumber].Tell(new StationActor.UserArrivedMessage(message.UsersName));
    }

    private void CreateChildIfNotExists(int stationNumber)
    {
        if (!_stationActors.ContainsKey(stationNumber))
            _stationActors.Add(stationNumber, Context.ActorOf(Props.Create(() => new StationActor(stationNumber)), $"Station{stationNumber}"));
    }

    private Dictionary<int, IActorRef> _stationActors;
    #region Messages

    public class UserArrivedMessage
    {
        public string UsersName { get; private set; }
        public int StationNumber { get; private set; }

        public UserArrivedMessage(string usersName, int stationNumber)
        {
            UsersName = usersName;
            StationNumber = stationNumber;
        }
    }

    public class UserLeftMessage
    {
        public string UsersName { get; private set; }
        public int StationNumber { get; private set; }

        public UserLeftMessage(string usersName, int stationNumber)
        {
            UsersName = usersName;
            StationNumber = stationNumber;
        }
    }
    #endregion

}