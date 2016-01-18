using System.Collections.Generic;
using Sample.Services.Models;

namespace Sample.Services
{
    public interface IUserService
    {
        IList<User> GetUsers();

        void AddUser(User user);
    }
}