using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Therapist.Core.Services;

namespace therapist.API.Attributes
{
    public class Cache : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTimeInMinutes;

        public Cache(int ExpireTimeInMinutes)
        {
            _expireTimeInMinutes = ExpireTimeInMinutes;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheKey = GenerateCacheFromReq(context.HttpContext.Request);
        var cacheRes =   await  CacheService.GetFromCache(CacheKey);
            if (cacheRes != null)
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheRes,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                context.Result = contentResult;
                return;
            }
             var EndpointContent =   await next.Invoke();
            if (EndpointContent.Result is OkObjectResult result)
            {
             await   CacheService.AddToCache(CacheKey, result.Value, TimeSpan.FromMinutes(_expireTimeInMinutes));
            }
            
        }

        private string GenerateCacheFromReq(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(X=>X.Key))
            {
                Key.Append($"{ key}-{ value}");
            }

            return Key.ToString();
        }
    }
}
