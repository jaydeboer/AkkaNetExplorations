using Xunit;
using Sample.Services.Impl;
using Sample.Services.Models;

namespace Sample.Service.Tests
{
    /// <summary>
    /// Testing the Sample User Service
    /// <see cref="http://xunit.github.io/docs/getting-started-dnx.html"/>
    /// </summary>
    public class SampleUserServiceTests
    {
        [Fact]
        public void NoUsersToStart()
        {
            Assert.Equal(new SampleUserService().GetUsers().Count, 0);
        }

        [Fact]
        public void AddingUserShouldExist()
        {
            var service = new SampleUserService();
            service.AddUser(new User());

            Assert.Equal(service.GetUsers().Count, 1);
        }
    }
}