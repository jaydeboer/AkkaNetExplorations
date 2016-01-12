using Akka.Actor;
using Sample2.DataAccess;
using Sample2.Models;

namespace Sample2.Actors
{
    public class UserMoveCoordinatorActor : ReceiveActor
    {
        public UserMoveCoordinatorActor(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
            Receive<CreateMoveRequestMessage>(message => OnReceivedCreateMoveRequestMessage(message));
        }
        
        private readonly IRepositoryFactory RepositoryFactory;

        private void OnReceivedCreateMoveRequestMessage(CreateMoveRequestMessage message)
        {
            //TODO: move repository into another actor to isolate from failure.
            var entity = CreateRequestFromMessage(message);
            entity.State = MoveRequestState.Completed;

            //TODO: process message
        }

        private MoveRequest CreateRequestFromMessage(CreateMoveRequestMessage message)
        {
            var repo = RepositoryFactory.GetMoveRequestRepository();
            var entity = repo.Create(new MoveRequest()
            {
                UsersName = message.UsersName,
                FromStationNumber = message.FromStationNumber,
                ToStationNumber = message.ToStationNumber,
                State = MoveRequestState.Requested
            });
            return entity;
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