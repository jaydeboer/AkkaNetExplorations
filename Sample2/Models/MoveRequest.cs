namespace Sample2.Models
{
    public class MoveRequest
    {
        public string UsersName { get; set; }
        public int FromStationNumber { get; set; }
        public int ToStationNumber { get; set; }
        public MoveRequestState State { get; set; }
        public int Id { get; internal set; }
    }

    public enum MoveRequestState
    {
        Requested = 1,
        Accepted = 2,
        Rejected = 3,
        Completed = 4
    }
}