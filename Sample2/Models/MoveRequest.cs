namespace Sample2.Models
{
    public class MoveRequest
    {
        public string UsersName { get; set; }
        public int FromStationNumber { get; set; }
        public int ToStationNumber { get; set; }
        public int Id { get; internal set; }
    }
}