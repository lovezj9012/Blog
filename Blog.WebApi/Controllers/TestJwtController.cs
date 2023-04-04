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
    }
}
