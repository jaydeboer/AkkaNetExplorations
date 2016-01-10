using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

public class StationActor : ReceiveActor
{
    public int Id { get; private set; }

    public string[] CurrentUsers
    {
        get { return _currentUsers.ToArray(); }
    }

    // Constructor
    public StationActor(int id)
    {
        Id = id;
        _currentUsers = new HashSet<string>();

        Console.WriteLine($"Creating actor for station {id}");

        // define what messages an actor will act upon
        Receive<StationUserSendMessage>(m => OnReceivedStationUserSendMessage(m));
        Receive<UserLeftMessage>(m => OnReceivedStationUserLeftMessage(m));
        Receive<UserArrivedMessage>(m => OnReceivedUserArrivedMessage(m));
        Receive<StationListUsersRequestMessage>(m => OnReceivedStationListUsersRequestMessage());

    }

    private readonly HashSet<string> _currentUsers;

    private void OnReceivedStationUserSendMessage(StationUserSendMessage message)
    {
        Console.WriteLine($"Send performed by {message.UsersName} at station {Id}");
    }

    private void OnReceivedUserArrivedMessage(UserArrivedMessage message)
    {
        _currentUsers.Add(message.UsersName);
        Console.WriteLine($"The user {message.UsersName} has arrived at station {Id}");
    }

    private void OnReceivedStationUserLeftMessage(UserLeftMessage message)
    {
        _currentUsers.Remove(message.Name);
        Console.WriteLine($"The user {message.Name} has left station {Id}");
    }

    private void OnReceivedStationListUsersRequestMessage()
    {
        Sender.Tell(CurrentUsers);
    }

    #region Messages

    public class UserArrivedMessage
    {
        public string UsersName { get; private set; }

        public UserArrivedMessage(string usersName)
        {
            UsersName = usersName;
        }
    }

    public class UserLeftMessage
    {
        public string Name { get; private set; }

        public UserLeftMessage(string usersName)
        {
            Name = usersName;
        }
    }
    #endregion
}