using ChatSystem_Application.Contracts.IGenericRepository;
using ChatSystem_Application.Contracts.Irepository.message;
using ChatSystem_persistence.DataBaseConfig;
using ChatSystem_persistence.Repositories;
using ChatSystem_persistence.Repositories.message;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ChatSystem_persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            #region MongoDb configuration
            /// <summary>
            /// Configures MongoDB persistence services for dependency injection
            /// </summary>
            /// <param name="services">The IServiceCollection to add services to</param>
            /// <param name="configuration">Application configuration</param>
            /// <returns>The configured IServiceCollection</returns>

            // Load MongoDB settings from configuration
            var mongoDBSettings = new MongoDBSettings();
            configuration.GetSection("MongoDBSettings").Bind(mongoDBSettings);

            // Register MongoDB settings as singleton
            services.AddSingleton(mongoDBSettings);

            // Register MongoDB client as singleton
            // This client will be shared across all database operations
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<MongoDBSettings>();
                return new MongoClient(settings.WriteDatabase.ConnectionString);
            });

            // Register Write Database as singleton
            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                var settings = serviceProvider.GetRequiredService<MongoDBSettings>();
                return client.GetDatabase(settings.WriteDatabase.DatabaseName);
            });

            // Register Read Database as singleton
            services.AddSingleton(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                var settings = serviceProvider.GetRequiredService<MongoDBSettings>();
                return client.GetDatabase(settings.ReadDatabase.DatabaseName);
            });
            #endregion

            #region repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<Imessage_rep, Message_rep>();
            #endregion

            return services;
        }
    }
}