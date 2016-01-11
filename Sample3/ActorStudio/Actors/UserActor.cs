using System;
using System.Threading.Tasks;
using Akka.Actor;
using Sample.Services.Models;
using Sample.Services;

public class UserActor : ReceiveActor
{
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
        var previousStation = _user.CurrentStation;
        if (!_user.CurrentStation.HasValue || _user.CurrentStation.Value != message.StationId)
        {
            if (previousStation.HasValue)
            {
                var oldStationActor = Context.ActorSelection($"/user/StationCoordinatorActor/Station{previousStation.Value}");
                oldStationActor.Tell(new StationUserLeftMessage(_user.Name));
            }

            _user.CurrentStation = message.StationId;

            Console.WriteLine($"Send performed by {_user.Name} at station {_user.CurrentStation.Value}");

            var selection = Context.ActorSelection($"/user/StationCoordinatorActor");
            selection.Tell(new StationUserSendMessage(message.StationId, _user.Name));
        }
    }

    private void OnReceiveAssignUserMessage(User message)
    {
        _user = message;

        Become(Configured);
    }

}