using System;
using Akka.Actor;
using Sample2.Actors;

namespace Sample2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = ActorSystem.Create("MoveRequestActorSystem");
            var mra = system.ActorOf(Props.Create(() => new UserMoveCoordinatorActor()));
            var stationCoordinator = system.ActorOf(Props.Create(() => new StationCoordinatorActor()), "StationCoordinatorActor");

            mra.Tell(new CreateMoveRequestMessage("Jay", 1, 2));
            Console.Read();
            
            mra.Tell(new AcceptMoveRequestMessage(1));
            Console.Read();

            mra.Tell(new CompleteMoveRequestMessage(1));

            //mra.Tell(new RejectMoveRequestMessage(1));
            Console.Read();

            mra.Tell(new CreateMoveRequestMessage("Jay", 2, 3));
            Console.Read();
            
            mra.Tell(new RejectMoveRequestMessage(2));
            Console.Read();           

        }
    }
}
