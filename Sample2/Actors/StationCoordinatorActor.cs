using System.Collections.Generic;
using Akka.Actor;
using Sample2.Models;

namespace Sample2.Actors
{
    public class StationCoordinatorActor : ReceiveActor
    {
        public StationCoordinatorActor()
        {
            Receive<TransferRequestedMessage>(m => OnReceivedTranseferRequested(m));
            Receive<TransferAcceptedMessage>(m => OnReceivedTranseferAccepted(m));
            Receive<TransferRejectedMessage>(m => OnReceivedTranseferRejected(m));
            Receive<TransferCompletedMessage>(m => OnReceivedTranseferCompleted(m));
        }
        
        private readonly Dictionary<int, IActorRef> _stations = new Dictionary<int, IActorRef>();
        
        private void OnReceivedTranseferRequested(TransferRequestedMessage request)
        {
            var sourceStation = GetStation(request.Move.FromStationNumber);
            var destinationStation = GetStation(request.Move.ToStationNumber);
            sourceStation.Tell(new StationActor.TransferOutRequestedMessage(request.Move.UsersName));
            destinationStation.Tell(new StationActor.TransferInRequestedMessage(request.Move.UsersName));
        }
        
        private void OnReceivedTranseferAccepted(TransferAcceptedMessage request)
        {
            var sourceStation = GetStation(request.Move.FromStationNumber);
            var destinationStation = GetStation(request.Move.ToStationNumber);
            sourceStation.Tell(new StationActor.TransferOutAcceptedMessage(request.Move.UsersName));
            destinationStation.Tell(new StationActor.TransferInAcceptedMessage(request.Move.UsersName));
        }
        
        private void OnReceivedTranseferRejected(TransferRejectedMessage request)
        {
            var sourceStation = GetStation(request.Move.FromStationNumber);
            var destinationStation = GetStation(request.Move.ToStationNumber);
            var rejectMessage = new StationActor.TransferRequestRejectedMessage(request.Move.UsersName);
            sourceStation.Tell(rejectMessage);
            destinationStation.Tell(rejectMessage);
        }
        
        private void OnReceivedTranseferCompleted(TransferCompletedMessage request)
        {
            var sourceStation = GetStation(request.Move.FromStationNumber);
            var destinationStation = GetStation(request.Move.ToStationNumber);
            var completeMessage = new StationActor.TransferCompletedMessage(request.Move.UsersName);
            sourceStation.Tell(completeMessage);
            destinationStation.Tell(completeMessage);
        }

        private IActorRef GetStation(int stationNumber)
        {
            if (!_stations.ContainsKey(stationNumber))
            {
                _stations.Add(stationNumber, Context.ActorOf(Props.Create(() => new StationActor(stationNumber)), $"Station{stationNumber}"));
            }
            return _stations[stationNumber];
        }

        #region Messages

        public class TransferRequestedMessage
        {
            public MoveRequest Move {get; private set;} 
            
            public TransferRequestedMessage(MoveRequest move)
            {
                Move = move;
            }   
        }
        public class TransferAcceptedMessage
        {
            public MoveRequest Move {get; private set;} 
            
            public TransferAcceptedMessage(MoveRequest move)
            {
                Move = move;
            }   
        }
        public class TransferRejectedMessage
        {
            public MoveRequest Move {get; private set;} 
            
            public TransferRejectedMessage(MoveRequest move)
            {
                Move = move;
            }   
        }
        public class TransferCompletedMessage
        {
            public MoveRequest Move {get; private set;} 
            
            public TransferCompletedMessage(MoveRequest move)
            {
                Move = move;
            }   
        }
        
        #endregion
    }
}