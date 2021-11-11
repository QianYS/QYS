using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace QYS_Project.Helper
{
    public class QYSAuthorizeAttribute : IAuthorizationFilter
    {
        private readonly ILogger<QYSAuthorizeAttribute> _logger;
        public QYSAuthorizeAttribute(ILogger<QYSAuthorizeAttribute> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedResult();//默认不认通过，保证sdk获取异常也是返回401

            if (!context.HttpContext.User.Identity.IsAuthenticated)
                return;

            //获取当前请求id
            var request = context.HttpContext.Request;

            var url = GetUrlRoute(request);

            var method = request.Method;
        }

        private static string GetUrlRoute(HttpRequest request)
        {
            var url = request.Path.Value.Substring(1);

            var dicRoute = request.RouteValues;
            foreach (var (key, o) in dicRoute)
            {
                if (key == "action" || key == "controller") 
                    continue;

                var value = o.ToString();
                url = url.Replace(value, string.Concat("{", key, "}"));
                break;
            }

            return url;
        }
    }
}
