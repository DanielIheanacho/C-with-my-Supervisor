using NUnit.Framework;
using System.Security.Claims;

namespace SchoolManagement.Tests.AuthorizationTest
{
    internal class IsMeHandlerTest
    {
        [Test]
        public async Task HandleRequirementAsync_WhenUserIdsMatch_Succeeds()
        {
            //Arrange
            var claim = new Claim(ClaimTypes.NameIdentifier, "5");
            var identity = new ClaimsIdentity(new[] { claim });
        }
    }
}
