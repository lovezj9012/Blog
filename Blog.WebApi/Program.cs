using Blog.IRepository;
using Blog.IService;
using Blog.Repository;
using Blog.Service;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();



static class IOCExtend
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
}
