using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utils.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogTypeController : ControllerBase
    {
        private readonly IBlogTypeService blogTypeService;

        public BlogTypeController(IBlogTypeService _blogTypeService)
        {
            this.blogTypeService = _blogTypeService;
        }

        [HttpGet("GetBlogTypes")]
        public async Task<ActionResult<ApiResult>> GetBlogTypes()
        {
            var data = await blogTypeService.QueryAsync();
            if (data == null)
            {
                return ApiResultHelper.Error("未查询到数据！");
            }
            return ApiResultHelper.Success(data);
        }

        [HttpPost("AddBlogType")]
        public async Task<ActionResult<ApiResult>> AddBlogType(string typeName)
        {
            BlogType blogType = new BlogType();
            blogType.TypeName = typeName;
            bool flag =await blogTypeService.AddAsync(blogType);
            if (!flag)
            {
                return ApiResultHelper.Error("添加类型失败！");
            }
            return ApiResultHelper.Success(flag);
        }

        [HttpPut("EditBlogType")]
        public async Task<ActionResult<ApiResult>> EditBlogType(string typeName,int id)
        {
            BlogType blogType = await blogTypeService.FindByIdAsync(id);
            if (blogType == null)
            {
                return ApiResultHelper.Error("未查询到数据！");
            }
            blogType.TypeName = typeName;
            bool flag = await blogTypeService.EditAsync(blogType);
            if (!flag)
            {
                return ApiResultHelper.Error("修改类型失败！");
            }
            return ApiResultHelper.Success(flag);
        }

        [HttpDelete("DeleteBlogType")]
        public async Task<ActionResult<ApiResult>> DeleteBlogType(int id)
        {
            bool flag = await blogTypeService.DeleteAsync(id);
            if (!flag)
            {
                return ApiResultHelper.Error("删除类型失败！");
            }
            return ApiResultHelper.Success(flag);
        }
    }
}
