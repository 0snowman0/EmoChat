using Analys.api.contracts.BackgroundService;
using Microsoft.Extensions.Hosting;

namespace Analys.api.Implenemetation.BackgroundService
{
    public class TimedBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int _intervalMinutes;

        public TimedBackgroundService(IServiceProvider serviceProvider, int intervalMinutes)
        {
            _serviceProvider = serviceProvider;
            _intervalMinutes = intervalMinutes;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var task = scope.ServiceProvider.GetRequiredService<IScheduledTask>();

                try
                {
                    await task.ExecuteAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"خطا در اجرای وظیفه: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes), stoppingToken);
            }
        }
    }

}
