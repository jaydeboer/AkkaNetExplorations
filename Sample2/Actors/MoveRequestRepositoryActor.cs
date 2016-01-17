using Akka.Actor;
using Sample2.DataAccess;
using Sample2.Models;

namespace Sample2.Actors
{
    public class MoveRequestRepositoryActor : ReceiveActor
    {
        public MoveRequestRepositoryActor(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
            
            Receive<CreateMessage>(message => OnReceivedCreateMessage(message));
        }

        private void OnReceivedCreateMessage(CreateMessage message)
        {
            Sender.Tell(CreateRequestFromMessage(message));
        }

        private readonly IRepositoryFactory RepositoryFactory;

        private MoveRequest CreateRequestFromMessage(CreateMessage message)
        {
            var repo = RepositoryFactory.GetMoveRequestRepository();
            var entity = repo.Create(new MoveRequest()
            {
                UsersName = message.UsersName,
                FromStationNumber = message.FromStationNumber,
                ToStationNumber = message.ToStationNumber,
                //State = MoveRequestState.Requested
            });
            return entity;
        }
        
        public class CreateMessage
        {
            public string UsersName { get; private set; }
            public int FromStationNumber { get; private set; }
            public int ToStationNumber { get; private set; }

            public CreateMessage(string usersName, int fromStationNumber, int toStationNumber)
            {
                UsersName = usersName;
                FromStationNumber = fromStationNumber;
                ToStationNumber = toStationNumber;
            }
        }
    }
}