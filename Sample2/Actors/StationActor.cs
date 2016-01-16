using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;

namespace Sample2.Actors
{
    public class StationActor : ReceiveActor
    {
        public StationActor(int id)
        {
            Id = id;

            //setup the message handlers
            Receive<TransferInRequestedMessage>(m => OnReceiveTransferInRequested(m));
            Receive<TransferOutRequestedMessage>(m => OnReceiveTransferOutRequested(m));
            Receive<TransferInAcceptedMessage>(m => OnReceiveTransferInAccepted(m));
            Receive<TransferOutAcceptedMessage>(m => OnReceiveTransferOutAccepted(m));
            Receive<TransferCompletedMessage>(m => OnReceiveTransferCompleted(m));
            Receive<TransferRequestRejectedMessage>(m => OnReceiveTransferRequestRejected(m));
        }

        private readonly List<string> _activeUsers = new List<string>();
        private readonly List<string> _requestedUsers = new List<string>();
        private readonly List<string> _incommingUsers = new List<string>();
        private readonly List<string> _leavingUsers = new List<string>();
        private readonly int Id;

        private void OnReceiveTransferInRequested(TransferInRequestedMessage message)
        {
            _requestedUsers.Add(message.User);
            PrintDetails();
        }

        private void OnReceiveTransferOutRequested(TransferOutRequestedMessage message)
        {
            _requestedUsers.Add(message.User);
            PrintDetails();
        }

        private void OnReceiveTransferInAccepted(TransferInAcceptedMessage message)
        {
            _incommingUsers.Add(message.User);
            _requestedUsers.Remove(message.User);
            PrintDetails();
        }

        private void OnReceiveTransferOutAccepted(TransferOutAcceptedMessage message)
        {
            _leavingUsers.Add(message.User);
            _requestedUsers.Remove(message.User);
            PrintDetails();
        }

        private void OnReceiveTransferCompleted(TransferCompletedMessage message)
        {
            if (_incommingUsers.Contains(message.User))
            {
                _incommingUsers.Remove(message.User);
                _activeUsers.Add(message.User);
            }
            else
            {
                _leavingUsers.Remove(message.User);
                _activeUsers.Remove(message.User);
            }
            PrintDetails();
        }

        private void OnReceiveTransferRequestRejected(TransferRequestRejectedMessage message)
        {
            _requestedUsers.Remove(message.User);
            PrintDetails();
        }

        private void PrintDetails()
        {
            Console.WriteLine($"Current info for station {Id}:");
            Console.WriteLine($"Active Users: {FriendlyList(_activeUsers)}");
            Console.WriteLine($"Users requested to move: {FriendlyList(_requestedUsers)}");
            Console.WriteLine($"Users that are leaving: {FriendlyList(_leavingUsers)}");
            Console.WriteLine($"Users that are inbound: {FriendlyList(_incommingUsers)}");
            Console.WriteLine();
        }
        
        private string FriendlyList(List<string> list)
        {
            if (list.Any())
            {
                return string.Join(", ", list);
            }
            else
            {
                return "None";
            }
        }
    }

    #region Messages

    public class TransferInRequestedMessage
    {
        public string User { get; private set; }

        public TransferInRequestedMessage(string user)
        {
            User = user;
        }
    }

    public class TransferOutRequestedMessage
    {
        public string User { get; private set; }

        public TransferOutRequestedMessage(string user)
        {
            User = user;
        }
    }

    public class TransferInAcceptedMessage
    {
        public string User { get; private set; }
        TransferInAcceptedMessage(string user)
        {
            User = user;
        }
    }

    public class TransferOutAcceptedMessage
    {
        public string User { get; private set; }

        public TransferOutAcceptedMessage(string user)
        {
            User = user;
        }
    }

    public class TransferCompletedMessage
    {
        public string User { get; private set; }
        TransferCompletedMessage(string user)
        {
            User = user;
        }
    }

    public class TransferRequestRejectedMessage
    {
        public string User { get; private set; }

        public TransferRequestRejectedMessage(string user)
        {
            User = user;
        }
    }

    #endregion
}