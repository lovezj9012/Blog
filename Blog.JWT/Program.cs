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

builder.Services.AddSqlSugar(new IocConfig()
{
    DbType = IocDbType.MySql,
    ConnectionString = builder.Configuration.GetSection("dbName").Value,
    IsAutoCloseConnection = true
}); ;

builder.Services.ConfigurationSugar(db =>
{
    db.Aop.OnLogExecuting = (sql, param) =>
    {
        Console.WriteLine(sql);
    };
});

builder.Services.AddIoc();

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


static class IocExtends
{
    public static IServiceCollection AddIoc(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthorService, AuthorService>();
        return services;
    }
}
