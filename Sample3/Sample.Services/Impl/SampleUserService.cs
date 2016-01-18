using System.Collections.Generic;
using Sample.Services.Models;

namespace Sample.Services.Impl
{
    public class SampleUserService : IUserService
    {
        public void AddUser(User user)
        {
            if (!_users.ContainsKey(user.Id))
            {
                _users[user.Id] = user;
            }
        }

        public IList<User> GetUsers()
        {
            return new List<User>(_users.Values);
        }

        // Constructor
        public SampleUserService()
        {
            _users = new Dictionary<int, User>();
        }

        private IDictionary<int, User> _users;
    }
}