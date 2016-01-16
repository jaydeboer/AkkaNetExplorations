using System.Collections.Generic;
using Akka.Actor;

namespace Sample2.Actors
{
    public class StationActor : ReceiveActor
    {
        public StationActor()
        {
            
        }
        
        private readonly List<string> _activeUsers = new List<string>();
        private readonly List<string> _requestedUsers = new List<string>();
        private readonly List<string> _incommingUsers = new List<string>();
        private readonly List<string> _leavingUsers = new List<string>();
    }
    
    #region Messages
    
    public class TransferInRequestedMessage
    {
        public string User {get; private set;}
        
        public TransferInRequestedMessage(string user)
        {
            User = user;
        }
    }
    
    public class TransferInAcceptedMessage
    {
        public string User {get; private set;}
        TransferInAcceptedMessage(string user)
        {
            User = user;
        }
    }
    
    public class TransferOutAcceptedMessage
    {
        public string User {get; private set;}
        
        public TransferOutAcceptedMessage(string user)
        {
            User = user;
        }
    }
    
    public class TransferInCompletedMessage
    {
        public string User {get; private set;}
        TransferInCompletedMessage(string user)
        {
            User = user;
        }
    }
    
    public class TransferOutCompletedMessage
    {
        public string User {get; private set;}
        
        public TransferOutCompletedMessage(string user)
        {
            User = user;
        }
    }
    
    #endregion
}