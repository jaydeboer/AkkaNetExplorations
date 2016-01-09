using System;
using Akka.Actor;

public class UserActor : UntypedActor
{
    protected override void OnReceive(object message)
    {
        Console.WriteLine(message.ToString());
    }
}