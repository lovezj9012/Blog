using Blog.IRepository;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using Blog.WebApi.Utils._AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.WebApi", Version = "v1" });
    #region Swagger 使用鉴权组件
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
    #endregion

});


#region 配置数据库
builder.Services.AddSqlSugar(new IocConfig()
{
    ConnectionString = builder.Configuration.GetSection("dbName").Value,
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true//自动释放
});

//设置参数
builder.Services.ConfigurationSugar(db =>
{
    db.Aop.OnLogExecuting = (sql, p) =>
    {
        Console.WriteLine(sql);
    };
    //设置更多连接参数
    //db.CurrentConnectionConfig.XXXX=XXXX
    //db.CurrentConnectionConfig.MoreSetting=new MoreSetting(){}
    //读写分离等都在这儿设置
});
#endregion

#region ioc注入
builder.Services.AddCustomIOC();
#endregion

#region JWT鉴权
builder.Services.AddCustomJwt();
#endregion

builder.Services.AddAutoMapper(typeof(CustomeAutoMapperProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//添加到管道中 JWT鉴权
app.UseAuthentication();
//授权
app.UseAuthorization();

app.MapControllers();

app.Run();



static class CustomeExtend
{
    public static IServiceCollection AddCustomIOC(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
        services.AddScoped<IBlogTypeRepository, BlogTypeRepository>();

        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBlogNewsService, BlogNewsService>();
        services.AddScoped<IBlogTypeService, BlogTypeService>();
        return services;
    }

    public static IServiceCollection AddCustomJwt(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
        {
            op.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D12BC3DF-5785-4465-9AFE-499CC9AE223A")),
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8000",
                ValidateAudience = true,
                ValidAudience = "http://localhost:5268",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(60)
            };
        });
        return services;
    }
}
