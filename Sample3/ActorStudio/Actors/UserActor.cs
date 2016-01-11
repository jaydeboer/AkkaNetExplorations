using System;
using System.Threading.Tasks;
using Akka.Actor;
using Sample.Services.Models;
using Sample.Services;

public class UserActor : ReceiveActor
{
    public int? CurrentStation { get; private set; }
    public string Name { get; private set; }

    // Constructor
    public UserActor(IUserService userService)
    {
        _userService = userService;

        // define what messages an actor will act upon
        Unconfigured();
    }

    private User _user;
    private IUserService _userService;

    #region Actor states
    private void Unconfigured()
    {
        Receive<User>(message => OnReceiveAssignUserMessage(message));
    }

    private void Configured()
    {
        Receive<SendPerformedMessage>(message => OnReceivedSendPerformedMessage(message));
    }
    #endregion


    private void OnReceivedSendPerformedMessage(SendPerformedMessage message)
    {
        var previousStation = CurrentStation;
        if (!CurrentStation.HasValue || CurrentStation.Value != message.StationId)
        {
            if (previousStation.HasValue)
            {
                var oldStationActor = Context.ActorSelection($"/user/StationCoordinatorActor/Station{previousStation.Value}");
                oldStationActor.Tell(new StationUserLeftMessage(Name));
            }

            CurrentStation = message.StationId;

            Console.WriteLine($"Send performed by {Name} at station {CurrentStation.Value}");

            var selection = Context.ActorSelection($"/user/StationCoordinatorActor");
            selection.Tell(new StationUserSendMessage(message.StationId, Name));
        }
    }

    private void OnReceiveAssignUserMessage(User message)
    {
        _user = message;

        Become(Configured);
    }

}