using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace YEG.Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception.GetType() == typeof(BusinessException)) 
                return CreateBusinessException(context, exception);
            if (exception.GetType() == typeof(AuthorizationException))
                return CreateAuthorizationException(context, exception);
            return CreateInternalException(context, exception);
        }

        private Task CreateAuthorizationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new AuthorizationProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://yeg-core.com/probs/authorization",
                Title = "Authorization exception",
                Detail = exception.Message,
                Instance = context.Request.Path
            }.ToString());
        }

        private Task CreateBusinessException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://yeg-core.com/probs/business",
                Title = "Business exception",
                Detail = exception.Message,
                Instance = context.Request.Path
            }.ToString());
        }

        private Task CreateInternalException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            return context.Response.WriteAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://yeg-core.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                Instance = context.Request.Path
            }.ToString());
        }
    }
}
