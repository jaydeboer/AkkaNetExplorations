using System.Collections.Generic;

namespace Sample.Services.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<string> ActiveUsers { get { return _activeUsers; } }

        public Station()
        {
            _activeUsers = new List<string>();
        }

        private List<string> _activeUsers;
    }
}