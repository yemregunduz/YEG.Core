using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using YEG.Core.Pipelines.Authorization;
using YEG.Core.CrossCuttingConcerns.Exceptions;

namespace YEG.Core.Tests.Pipelines.Authorization
{
    public class AuthorizationBehaviorTests
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<HttpContext> _httpContextMock;

        public AuthorizationBehaviorTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _httpContextMock = new Mock<HttpContext>();
        }

        [Fact]
        public async Task Handle_WithAuthorizedRequest_ShouldReturnResponse()
        {

            CreateHttpContextMockWithClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Role1"),
                new Claim(ClaimTypes.Role, "Role2"),
                new Claim(ClaimTypes.Role, "Role3")
            });

            var request = new SecuredRequest();
            var behavior = new AuthorizationBehavior<SecuredRequest, string>(_httpContextAccessorMock.Object);

            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            var result = await behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None);

            Assert.Equal("Response", result);
        }

        [Fact]
        public async Task Handle_WithUnauthorizedRequest_ShouldThrowAuthorizationException()
        {

            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(_httpContextMock.Object);

            var request = new SecuredRequest();
            var behavior = new AuthorizationBehavior<SecuredRequest, string>(_httpContextAccessorMock.Object);

            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            await Assert.ThrowsAsync<AuthorizationException>(() => behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNoMatchingRoles_ShouldThrowAuthorizationException()
        {

            CreateHttpContextMockWithClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Role4"),
            });

            var request = new SecuredRequest();
            var behavior = new AuthorizationBehavior<SecuredRequest, string>(_httpContextAccessorMock.Object);

            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            await Assert.ThrowsAsync<AuthorizationException>(() => behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithMatchingAnyRoles_ShouldReturnResponse()
        {
            CreateHttpContextMockWithClaims(new List<Claim>
            {
                new Claim(ClaimTypes.Role, "Role2")
            });

            var request = new SecuredRequest();
            var behavior = new AuthorizationBehavior<SecuredRequest, string>(_httpContextAccessorMock.Object);
                
            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            var result = await behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None);

            Assert.Equal("Response", result);
        }

        [Theory]
        [MemberData(nameof(UserClaims))]
        public async Task Handle_WithEmptyRequestRolesAndEmptyClaimsOrNot_ShouldReturnResponse(List<Claim> userClaims)
        {

            CreateHttpContextMockWithClaims(userClaims);

            var behavior = new AuthorizationBehavior<EmptyRequestRolesRequest, string>(_httpContextAccessorMock.Object);

            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            var request = new EmptyRequestRolesRequest();

            var result = await behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None);

            Assert.Equal("Response", result);
        }

        [Theory]
        [MemberData(nameof(UserClaims))]
        public async Task Handle_WithNullRequestRolesAndEmptyClaimsOrNot_ShouldReturnResponse(List<Claim> userClaims)
        {
            CreateHttpContextMockWithClaims(userClaims);

            var behavior = new AuthorizationBehavior<NullRequestRolesRequest, string>(_httpContextAccessorMock.Object);

            var nextHandlerMock = new Mock<RequestHandlerDelegate<string>>();
            nextHandlerMock.Setup(h => h()).ReturnsAsync("Response");

            var request = new NullRequestRolesRequest();

            var result = await behavior.Handle(request, nextHandlerMock.Object, CancellationToken.None);

            Assert.Equal("Response", result);
        }

        public static IEnumerable<object[]> UserClaims()
        {
            yield return new object[]
            {
                new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Role1"),
                    new Claim(ClaimTypes.Role, "Role2"),
                    new Claim(ClaimTypes.Role, "Role3")
                }
            };

            yield return new object[]
            {
                new List<Claim>(){}
            };

            yield return new object[]
            {
                null
            };
        }

        private void CreateHttpContextMockWithClaims(List<Claim> userClaims)
        {
            var claimsIdentity = new ClaimsIdentity(userClaims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _httpContextMock.Setup(c => c.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(_httpContextMock.Object);
        }
    }
}
