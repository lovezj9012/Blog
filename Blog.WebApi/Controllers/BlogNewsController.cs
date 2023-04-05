using AutoMapper;
using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utils.ApiResult;
using Blog.WebApi.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService blogNewsService;

        public BlogNewsController(IBlogNewsService _blogNewsService)
        {
            blogNewsService = _blogNewsService;
        }

        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var data = await blogNewsService.QueryAsync(b => b.AuthorId == id);
            if (data == null)
            {
                return ApiResultHelper.Error("未查询到数据！");
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("BlogNew")]
        public async Task<ActionResult<ApiResult>> AddBlogNews(BlogNewsDto blog)
        {
            BlogNews blogNews = new BlogNews();
            blogNews.Title = blog.Title;
            blogNews.Content = blog.Content;
            blogNews.TypeId = blog.TypeId;
            blogNews.Time = DateTime.Now;
            blogNews.AuthorId = Convert.ToInt32(this.User.FindFirst("Id").Value);
            bool flag = await blogNewsService.AddAsync(blogNews);
            if (!flag)
            {
                return ApiResultHelper.Error("添加失败，服务发生错误！");
            }
            return ApiResultHelper.Success(flag);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> DeleteBlogNews(int id)
        {
            bool flag = await blogNewsService.DeleteAsync(id);
            if (!flag)
            {
                return ApiResultHelper.Error("删除失败！");
            }
            return ApiResultHelper.Success(flag);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ApiResult>> UpdateBlogNews(int id, string title, string content, int typeId)
        {
            var blogNews = await blogNewsService.FindByIdAsync(id);
            if (blogNews == null)
            {
                return ApiResultHelper.Error("没有找到数据！");
            }
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeId;
            bool flag = await blogNewsService.EditAsync(blogNews);
            if (!flag)
            {
                return ApiResultHelper.Error("删除失败！");
            }
            return ApiResultHelper.Success(flag);
        }

        [HttpGet("GetPage")]
        public async Task<ApiResult> GetPage([FromServices] IMapper iMapper,int page,int size)
        {
            int total = 0;
            PageModel model = new PageModel
            {
                PageIndex = page,
                PageSize = size,
                TotalCount = total
            };
            var blogNews = await blogNewsService.PageAsync(c => c.Id == c.Id, model);
            var blogDto = iMapper.Map<List<BlogNewsDto>>(blogNews);
            return ApiResultHelper.Success(blogDto, model.TotalCount);
        }


    }
}
