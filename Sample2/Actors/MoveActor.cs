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
            
            Receive<AcceptMessage>(message => {
                Console.WriteLine($"move {_model.Id} accepted.");
                _stationCoordinator.Tell(new StationCoordinatorActor.TransferAcceptedMessage(_model));
            });
            Receive<RejectMessage>(message => 
            {
                Console.WriteLine($"move {_model.Id} rejected.");
                Self.Tell(PoisonPill.Instance);
            });
            Receive<CompleteMessage>(message => 
            {
                Console.WriteLine($"move {_model.Id} completed.");
                Self.Tell(PoisonPill.Instance);
            });
            
            _stationCoordinator.Tell(new StationCoordinatorActor.TransferRequestedMessage(_model));
        }

        private readonly MoveRequest _model;
        private readonly ActorSelection _stationCoordinator;
        
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