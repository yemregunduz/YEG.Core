using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Yeg.Utilities.Extensions.Claims;
using YEG.Core.CrossCuttingConcerns.Logging;
using YEG.Core.CrossCuttingConcerns.Logging.Serilog;

namespace YEG.Core.Pipelines.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ILoggableRequest
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(LoggerServiceBase loggerServiceBase, IHttpContextAccessor httpContextAccessor)
        {
            _loggerServiceBase = loggerServiceBase;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<LogParameter> logParameters = new()
            {
                new LogParameter
                {
                    Type = request.GetType().Name,
                    Value = request
                }
            };

            LogDetail logDetail = new()
            {
                MethodName = next.Method.Name,
                LogParameters = logParameters,
                UserId = _httpContextAccessor.HttpContext?.User?.GetUserId().ToString() ?? "?"
            };

            _loggerServiceBase.Info(JsonSerializer.Serialize(logDetail));

            return next();
        }
    }
}
