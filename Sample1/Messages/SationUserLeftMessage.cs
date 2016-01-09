public class StationUserLeftMessage
{
    public string Name {get; private set; }
    
    public StationUserLeftMessage(string usersName)
    {
        Name = usersName;
    }
}