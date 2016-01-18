using System;
using Akka.Actor;
using Sample2.Models;

namespace Sample2.Actors
{
    public class MoveActor : ReceiveActor
    {
        public MoveActor(MoveRequest request)
        {
            _model = request;
            _stationCoordinator = Context.ActorSelection($"/user/StationCoordinatorActor");
            Console.WriteLine($"move {_model.Id} created.");

            Receive<AcceptMessage>(m => OnReceivedAcceptMessage(m));
            Receive<RejectMessage>(m => OnReceivedRejectMessage(m));
            Receive<CompleteMessage>(m => OnReceivedCompleteMessage(m));

            _stationCoordinator.Tell(new StationCoordinatorActor.TransferRequestedMessage(_model));
        }

        private readonly MoveRequest _model;
        private readonly ActorSelection _stationCoordinator;

        private void OnReceivedAcceptMessage(AcceptMessage message)
        {
            Console.WriteLine($"move {_model.Id} accepted.");
            _stationCoordinator.Tell(new StationCoordinatorActor.TransferAcceptedMessage(_model));
        }

        private void OnReceivedRejectMessage(RejectMessage message)
        {
            Console.WriteLine($"move {_model.Id} rejected.");
            _stationCoordinator.Tell(new StationCoordinatorActor.TransferRejectedMessage(_model));
            Self.Tell(PoisonPill.Instance);
        }

        private void OnReceivedCompleteMessage(CompleteMessage message)
        {
            Console.WriteLine($"move {_model.Id} completed.");
            _stationCoordinator.Tell(new StationCoordinatorActor.TransferCompletedMessage(_model));
            Self.Tell(PoisonPill.Instance);
        }

        // private MoveRequestState State { get; set; }

        // public enum MoveRequestState
        // {
        //     Requested = 1,
        //     Accepted = 2,
        //     Rejected = 3,
        //     Completed = 4
        // }

        #region messages

        public class AcceptMessage { }
        public class RejectMessage { }
        public class CompleteMessage { }

        #endregion
    }
}