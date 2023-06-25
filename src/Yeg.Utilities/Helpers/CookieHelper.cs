using Microsoft.AspNetCore.Http;
using System;
using System.Web;
namespace Yeg.Utilities.Helpers
{
    public static class CookieHelper
    {

        public static void SetCookie(HttpContext httpContext, string name, string value, int? expireMinutes = null)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            var cookieOptions = new CookieOptions();

            if (expireMinutes.HasValue)
                cookieOptions.Expires = DateTime.Now.AddMinutes(expireMinutes.Value);

            httpContext.Response.Cookies.Append(name, value, cookieOptions);
        }

        public static string GetCookieValue(HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            return httpContext.Request.Cookies[name];
        }

        public static void DeleteCookie(HttpContext httpContext, string name)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext), "HttpContext cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cookie name cannot be null or empty.", nameof(name));

            httpContext.Response.Cookies.Delete(name);
        }
    }

}
