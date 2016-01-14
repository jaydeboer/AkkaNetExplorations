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
            Console.WriteLine($"move {_model.Id} created.");
            
            Receive<AcceptMessage>(message => Console.WriteLine($"move {_model.Id} accepted."));
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
        }

        private readonly MoveRequest _model;

        #region messages

        public class AcceptMessage { }
        public class RejectMessage { }
        public class CompleteMessage { }

        #endregion
    }
}