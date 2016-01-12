using Akka.Actor;
using Sample2.DataAccess;
using Sample2.Models;

namespace Sample2.Actors
{
    public class UserMoveCoordinatorActor : ReceiveActor
    {
        public UserMoveCoordinatorActor()
        {
            Receive<CreateMoveRequestMessage>(message => OnReceivedCreateMoveRequestMessage(message));
        }

        private void OnReceivedCreateMoveRequestMessage(CreateMoveRequestMessage message)
        {
            //TODO: move repository into another actor to isolate from failure.
            var db = Context.ActorOf(Props.Create(() => new MoveRequestRepositoryActor(new DataAccess.Impl.EFRepositoryFactory())));
            var moveRequest = db.Ask(new MoveRequestRepositoryActor.CreateMessage(message.UsersName, message.FromStationNumber, message.ToStationNumber));

            //TODO: process message
            
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
            FromStationNumber= fromStationNumber;
            ToStationNumber = toStationNumber;
        }
    }
    
    #endregion
}