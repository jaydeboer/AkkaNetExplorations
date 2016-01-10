using System;
using Akka.Actor;

namespace Sample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = ActorSystem.Create("UserActorSystem");
            IActorRef pickingActor = system.ActorOf(Props.Create(() => new PickingActor()), "PickingActor");

            pickingActor.Tell(new StationUserSendMessage(699, "Jay"));
            Console.Read();
            
            //var users = stationCoordinatorActor.Ask<string[]>(new StationListUsersRequestMessage(699)).Result;
            //Console.WriteLine($"The following user(s) are at station 699: {string.Join(", ", users)}");
            // Console.Read();

            pickingActor.Tell(new StationUserSendMessage(700, "Jay"));
            Console.Read();

            system.Shutdown();
            system.AwaitTermination();
            Console.WriteLine("Shutdown");
            Console.Read();
        }
    }
}
