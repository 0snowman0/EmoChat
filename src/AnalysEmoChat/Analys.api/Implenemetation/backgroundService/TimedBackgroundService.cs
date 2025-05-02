using Analys.api.contracts.BackgroundService;
using Microsoft.Extensions.Hosting;

namespace Analys.api.Implenemetation.BackgroundService
{
    public class TimedBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IScheduledTask _task;
        private readonly int _intervalMinutes;

        public TimedBackgroundService(IScheduledTask task, int intervalMinutes)
        {
            _task = task;
            _intervalMinutes = intervalMinutes;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _task.ExecuteAsync(stoppingToken);
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
