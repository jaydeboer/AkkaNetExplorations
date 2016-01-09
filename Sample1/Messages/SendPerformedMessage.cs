public class SendPerformedMessage
{
    public int StationId {get; private set;}
    
    public SendPerformedMessage(int atStation)
    {
        StationId = atStation;
    }
}