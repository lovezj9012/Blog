using Blog.WebApi.Utils._Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestJwtController : ControllerBase
    {
        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "No Authorize";
        }

        [HttpGet("Authorize")]
        [Authorize]
        public string Authorize()
        {
            return "Authorize";
        }

        [TypeFilter(typeof(CustomeResouceFilterAttribute))]
        [HttpGet("GetCache")]
        public IActionResult TestCache(string name)
        {
            return new JsonResult(new
            {
                name = "zhangsan",
                age = 13,
                sex = true
            });
        }
    }
}
