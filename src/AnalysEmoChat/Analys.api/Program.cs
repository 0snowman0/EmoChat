using Analys.api.config.database;
using Analys.api.contracts.AnalysUtilies;
using Analys.api.contracts.BackgroundService;
using Analys.api.contracts.Repository.mysql;
using Analys.api.contracts.Repository.mysql.UserEmojiUsage;
using Analys.api.contracts.Repository.redis;
using Analys.api.Implenemetation.AnalysUtilies;
using Analys.api.Implenemetation.BackgroundService;
using Analys.api.Implenemetation.Repository.mysql;
using Analys.api.Implenemetation.Repository.mysql.UserEmojiUsage;
using Analys.api.Implenemetation.Repository.redis;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddScoped<IScheduledTask, TransferRedisEmojiToMySqlTask>();

//builder.Services.AddHostedService(provider =>
//    new TimedBackgroundService(
//        provider,
//        intervalMinutes: 1
//    ));

#endregion

#region redis

var redisConnection = builder.Configuration["Redis:Connection"];

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnection));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnection;
    options.InstanceName = "EmoChat_";
});


#endregion

#region DI

//generic
builder.Services.AddScoped(typeof(IRedisRepository<>), typeof(RedisRepository<>));
builder.Services.AddScoped(typeof(IMySqlRepository<>), typeof(MySqlRepository<>));

builder.Services.AddScoped<IRedisUserEmoji , RedisUserEmoji>();
builder.Services.AddScoped<IUserEmojiUsage_Rep , UserEmojiUsage_Rep>();

builder.Services.AddScoped<IAnalysisProcessor , AnalysisProcessor>();


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
