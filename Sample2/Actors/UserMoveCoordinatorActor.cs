using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Sample2.Models;

namespace Sample2.Actors
{
    public class UserMoveCoordinatorActor : ReceiveActor
    {
        public UserMoveCoordinatorActor()
        {
            Receive<CreateMoveRequestMessage>(message => OnReceivedCreateMoveRequestMessage(message));
            Receive<AcceptMoveRequestMessage>(message => OnReceivedAcceptMoveRequestMessage(message));
            Receive<RejectMoveRequestMessage>(message => OnReceivedRejectMoveRequestMessage(message));
            Receive<CompleteMoveRequestMessage>(message => OnReceivedCompleteMoveRequestMessage(message));
        }

        private async Task OnReceivedCreateMoveRequestMessage(CreateMoveRequestMessage message)
        {
            var db = Context.ActorOf(Props.Create(() => new MoveRequestRepositoryActor(new DataAccess.Impl.EFRepositoryFactory())));
            var moveRequest = await db.Ask<MoveRequest>(new MoveRequestRepositoryActor.CreateMessage(message.UsersName, message.FromStationNumber, message.ToStationNumber));

            // Clean up the DB Actor
            db.Tell(PoisonPill.Instance, Self);

            _activeMoves.Add(moveRequest.Id, Context.ActorOf(Props.Create(() => new MoveActor(moveRequest)), $"Move{moveRequest.Id}"));
        }

        private void OnReceivedAcceptMoveRequestMessage(AcceptMoveRequestMessage message)
        {
            if (_activeMoves.ContainsKey(message.Id))
            {
                _activeMoves[message.Id].Tell(new MoveActor.AcceptMessage());
            }
        }

        private void OnReceivedRejectMoveRequestMessage(RejectMoveRequestMessage message)
        {
            if (_activeMoves.ContainsKey(message.Id))
            {
                _activeMoves[message.Id].Tell(new MoveActor.RejectMessage());
            }
            FinalizeMove(message.Id);
        }

        private void OnReceivedCompleteMoveRequestMessage(CompleteMoveRequestMessage message)
        {
            if (_activeMoves.ContainsKey(message.Id))
            {
                _activeMoves[message.Id].Tell(new MoveActor.CompleteMessage());
            }
            FinalizeMove(message.Id);
        }

        private Dictionary<int, IActorRef> _activeMoves;
        
        private void FinalizeMove(int id)
        {
            if (_activeMoves.ContainsKey(id))
                _activeMoves.Remove(id);
        }
    }

    #region Messages

    public class CreateMoveRequestMessage
    {
        public string UsersName { get; private set; }
        public int FromStationNumber { get; private set; }
        public int ToStationNumber { get; private set; }

        public CreateMoveRequestMessage(string usersName, int fromStationNumber, int toStationNumber)
        {
            UsersName = usersName;
            FromStationNumber = fromStationNumber;
            ToStationNumber = toStationNumber;
        }
    }

    public class AcceptMoveRequestMessage
    {
        public int Id { get; private set; }

        public AcceptMoveRequestMessage(int id)
        {
            Id = id;
        }
    }

    public class RejectMoveRequestMessage
    {
        public int Id { get; private set; }

        public RejectMoveRequestMessage(int id)
        {
            Id = id;
        }
    }

    public class CompleteMoveRequestMessage
    {
        public int Id { get; private set; }

        public CompleteMoveRequestMessage(int id)
        {
            Id = id;
        }
    }
    #endregion
}