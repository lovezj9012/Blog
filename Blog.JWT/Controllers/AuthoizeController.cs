using Blog.IService;
using Blog.JWT.Utils.ApiResult;
using Blog.JWT.Utils.MD5Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public AuthoizeController(IAuthorService _authorService)
        {
            this.authorService = _authorService;
        }

        [HttpGet]
        public async Task<ApiResult> Login(string name,string password)
        {
            string pwd = MD5Helper.MD5Encrypt32(password);
            var author = await authorService.FindAsync(u => u.AccountName == name && u.PassWord == pwd);
            if (author != null)
            {
                var claim = new Claim[] { 
                    new Claim(ClaimTypes.Name, name),
                    new Claim("Id",author.Id.ToString()),
                    new Claim("AccountName",author.AccountName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D12BC3DF-5785-4465-9AFE-499CC9AE223A"));
                //issuer表示颁发token的web应用，audiences表示token受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:8000",
                    audience: "http://localhost:5268",
                    claims: claim,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("用户名或密码错误！");
            }
            
        }
    }
}
