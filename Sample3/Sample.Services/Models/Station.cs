using System.Collections.Generic;

namespace Sample.Services.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<int> ActiveUsers { get { return _activeUsers; } }

        public Station()
        {
            _activeUsers = new List<int>();
        }

        private List<int> _activeUsers;
    }
}