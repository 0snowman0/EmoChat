using Analys.api.contracts.Repository;
using Analys.api.Implenemetation;
using Analys.api.model.settings;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





// ========== تنظیمات Redis از appsettings.json ==========
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
var redisSettings = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();

// ========== اتصال به Redis ==========
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(redisSettings.ConnectionString));

#region DI
builder.Services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));
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
