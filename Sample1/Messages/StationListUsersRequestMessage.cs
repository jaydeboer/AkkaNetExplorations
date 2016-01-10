public class StationListUsersRequestMessage
{
    public int StationId { get; private set; }

    public StationListUsersRequestMessage(int stationId)
    {
        StationId = stationId;
    }
}