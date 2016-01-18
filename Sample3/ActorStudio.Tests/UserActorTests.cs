using Akka.Actor;
using Xunit;

namespace ActorStudio.Tests
{
    /// <summary>
    /// Testing the UserActor
    /// <see cref="http://xunit.github.io/docs/getting-started-dnx.html"/>
    /// </summary>
    public class UserActorTests
    {
        [Fact]
        public void CanCreateUserActor()
        {
            var system = ActorSystem.Create("UserActorSystem");

            Assert.NotNull(system);
            //Assert.NotNull(system.ActorOf(Props.Create(() => new UserActor()), "TestActor"));

        }
    }
}
