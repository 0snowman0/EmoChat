using Analys.api.config.database;
using Analys.api.contracts.BackgroundService;
using Analys.api.contracts.Repository.mysql;
using Analys.api.contracts.Repository.mysql.UserEmojiUsage;
using Analys.api.contracts.Repository.redis;
using Analys.api.Implenemetation.BackgroundService;
using Analys.api.Implenemetation.Repository.mysql;
using Analys.api.Implenemetation.Repository.mysql.UserEmojiUsage;
using Analys.api.Implenemetation.Repository.redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region My Sql

var connectionString = builder.Configuration.GetSection("MySQL")["Connection"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

#endregion

#region Background service
builder.Services.AddSingleton<IScheduledTask, TransferRedisEmojiToMySqlTask>();
builder.Services.AddHostedService(provider =>
    new TimedBackgroundService(
        provider.GetRequiredService<IScheduledTask>(),
        intervalMinutes: 1
    ));
#endregion

#region redis

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "EmoChat_";
});

#endregion


#region DI

builder.Services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));
builder.Services.AddScoped(typeof(IMySqlRepository<>), typeof(MySqlRepository<>));

builder.Services.AddScoped<IRedisUserEmoji , RedisUserEmoji>();
builder.Services.AddScoped<IUserEmojiUsage_Rep , UserEmojiUsage_Rep>();




#endregion

#region MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
#endregion

#region Auto Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
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
