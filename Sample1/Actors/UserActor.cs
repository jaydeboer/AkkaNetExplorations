using System;
using Akka.Actor;

public class UserActor : UntypedActor
{
    protected override void OnReceive(object message)
    {
        if (message is string)
            Console.WriteLine(message.ToString());
        else if (message is int)
            Console.WriteLine($"I guess the answer is {message}");
    }
}