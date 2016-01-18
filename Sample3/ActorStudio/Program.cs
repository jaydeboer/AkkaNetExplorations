using System;
using Akka.Actor;
using Autofac;
using Sample.Services.Impl;
using Sample.Services;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Sample.Services.Models;

namespace Sample1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SampleUserService>().As<IUserService>();
            builder.RegisterType<SampleStationService>().As<IStationService>();
            builder.RegisterType<UserActor>();
            builder.RegisterType<StationCoordinatorActor>();
            builder.RegisterType<StationActor>();


            Container = builder.Build();

            var system = ActorSystem.Create("UserActorSystem");

            // Create the dependency resolver for the actor system
            var propsResolver = new AutoFacDependencyResolver(Container, system);

            IActorRef jayActor = system.ActorOf(system.DI().Props<UserActor>(), "Jay");
            jayActor.Tell(new User() { Id = 5, Name = "Jay" });
            IActorRef stationCoordinatorActor = system.ActorOf(system.DI().Props<StationCoordinatorActor>(), "StationCoordinatorActor");

            jayActor.Tell(new SendPerformedMessage(699));
            Console.Read();

            jayActor.Tell(new SendPerformedMessage(700));
            Console.Read();

            system.Shutdown();
            system.AwaitTermination();
            Console.WriteLine("Shutdown");
            Console.Read();
        }

        public static IContainer Container { get; private set; }
    }
}
