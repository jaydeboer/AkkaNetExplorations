using System.Collections.Generic;
using Akka.Actor;

public class UserStatsSendCountActor : ReceiveActor
{
    public UserStatsSendCountActor()
    {
        _stats = new Dictionary<string, int>();
        Receive<SendCompletedMessage>(m => OnReceivedSendCompletedMessage(m));
    }

    private readonly Dictionary<string, int> _stats;

    private void OnReceivedSendCompletedMessage(SendCompletedMessage message)
    {
        CreateChildIfNotExists(message.UsersName);
        _stats[message.UsersName]++; 
    }

    // Make sure we have send stats for the specified user
    private void CreateChildIfNotExists(string usersName)
    {
        if (!_stats.ContainsKey(usersName))
        {
            _stats.Add(usersName, 0);
        }
    }

    #region Messages
    public class SendCompletedMessage
    {
        public string UsersName { get; private set; }

        public SendCompletedMessage(string usersName)
        {
            UsersName = usersName;
        }
    }
    #endregion
}