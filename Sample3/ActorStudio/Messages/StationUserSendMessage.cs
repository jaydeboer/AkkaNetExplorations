
public class StationUserSendMessage
{
    public string UsersName { get; private set; }
    public int StationNumber { get; private set; }

    public StationUserSendMessage(int station, string usersName)
    {
        UsersName = usersName;
        StationNumber = station;
    }
}