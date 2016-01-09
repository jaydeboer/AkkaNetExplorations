using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;

namespace Sample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var system = ActorSystem.Create("UserActorSystem");
            var userActorProps = Props.Create<UserActor>();
            IActorRef actor = system.ActorOf(userActorProps);
            
            actor.Tell("Hello Universe!");
            
            Console.Read();
        }
    }
}
