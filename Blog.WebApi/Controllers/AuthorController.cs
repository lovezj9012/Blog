using Blog.IService;
using Blog.Model;
using Blog.WebApi.Utils.ApiResult;
using Blog.WebApi.Utils.MD5Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService _authorService)
        {
            this.authorService = _authorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetAuthors()
        {
            var data = await authorService.QueryAsync();
            if (data.Count == 0)
            {
                return ApiResultHelper.Error("未查询到数据！");
            }
            return ApiResultHelper.Success(data);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResult>> AddAuthor(string name, string username, string password)
        {
            Author author = new Author()
            {
                Name = name,
                AccountName = username,
                //加密密码
                PassWord = MD5Helper.MD5Encrypt32(password)
            };
            var exsitAuthor = await authorService.FindAsync(a => a.AccountName == username);
            if (exsitAuthor != null)
            {
                return ApiResultHelper.Error("账号已存在！");
            }
            bool flag = await authorService.AddAsync(author);
            if (!flag)
            {
                return ApiResultHelper.Error("添加失败！");
            }
            return ApiResultHelper.Success(author);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult>> EditAuthor(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            return ApiResultHelper.Error("未完结");
        }
    }
}
