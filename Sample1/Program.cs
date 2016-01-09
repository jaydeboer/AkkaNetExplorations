using System;
using Akka.Actor;

namespace Sample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = ActorSystem.Create("UserActorSystem");
            var userActorProps = Props.Create<UserActor>("Jay");
            var stationActorProps = Props.Create<StationActor>(699);
            IActorRef actor = system.ActorOf(userActorProps,"Jay");
            IActorRef stationActor = system.ActorOf(stationActorProps, "699");
            
            actor.Tell(new SendPerformedMessage(699));
            
            Console.Read();
        }
    }
}
