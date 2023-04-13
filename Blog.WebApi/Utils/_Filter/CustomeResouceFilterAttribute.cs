using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.WebApi.Utils._Filter
{
    public class CustomeResouceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache cache;
        public CustomeResouceFilterAttribute(IMemoryCache _cache)
        {
            cache = _cache;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string path = context.HttpContext.Request.Path; //api/controller/GetCache
            string route = context.HttpContext.Request.QueryString.Value; //?name = name值
            string key = path + route; //api/controller/GetCache?name=name值
            cache.Set(key, context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string path = context.HttpContext.Request.Path; //api/controller/GetCache
            string route = context.HttpContext.Request.QueryString.Value; //?name = name值
            string key = path + route; //api/controller/GetCache?name=name值
            if (cache.TryGetValue(key, out object value))
            {
                context.Result = value as IActionResult;
            }
        }
    }
}
