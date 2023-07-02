using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using YEG.Core.CrossCuttingConcerns.Exceptions;
using Yeg.Utilities.Extensions.Claims;

namespace YEG.Core.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ISecuredRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles() ?? throw new AuthorizationException("Claims Not Found");

            bool isAuthorized = 
                request.Roles is null || !request.Roles.Any() || request.Roles.Any(role => roleClaims?.Contains(role) == true);

            if (!isAuthorized)
                throw new AuthorizationException("You are not authorized.");

            TResponse response = await next();
            return response;
        }
    }
}
