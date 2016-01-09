using System;
using Akka.Actor;

namespace Sample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = ActorSystem.Create("UserActorSystem");
            IActorRef jayActor = UserActor.Create(system, "Jay");
            IActorRef stationCoordinatorActor = system.ActorOf(Props.Create(() => new StationCoordinatorActor()),"StationCoordinatorActor");
            
            jayActor.Tell(new SendPerformedMessage(699));
            Console.Read();
            jayActor.Tell(new SendPerformedMessage(700));
            Console.Read();
            
            system.Shutdown();
            system.AwaitTermination();
            Console.WriteLine("Shutdown");
            Console.Read();
        }
    }
}
