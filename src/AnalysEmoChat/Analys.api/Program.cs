using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Analys.api.config.settings;
using Analys.api.config.database;
using Microsoft.EntityFrameworkCore;
using Analys.api.contracts.Repository.redis;
using Analys.api.Implenemetation.Repository.redis;
using Analys.api.contracts.Repository.mysql;
using Analys.api.Implenemetation.Repository.mysql;
using Analys.api.contracts.BackgroundService;
using Analys.api.Implenemetation.BackgroundService;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Background service
builder.Services.AddSingleton<IScheduledTask, MyTask>();
builder.Services.AddHostedService(provider =>
    new TimedBackgroundService(
        provider.GetRequiredService<IScheduledTask>(),
        intervalMinutes: 1  
    ));
#endregion

#region redis
// ========== تنظیمات Redis از appsettings.json ==========
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
var redisSettings = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();

// ========== اتصال به Redis ==========
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(redisSettings.ConnectionString));
#endregion

#region MySql

var connectionString = builder.Configuration.GetSection("MySQL")["Connection"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

#endregion

#region DI

builder.Services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));
builder.Services.AddScoped(typeof(IMySqlRepository<>), typeof(MySqlRepository<>));

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
