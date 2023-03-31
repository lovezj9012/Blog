using Blog.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService blogNewsService;

        public BlogNewsController(IBlogNewsService _blogNewsService)
        {
            blogNewsService = _blogNewsService;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult> GetBlogNews()
        {
            var data = await blogNewsService.QueryAsync();
            return Ok(data);
        }
    }
}
