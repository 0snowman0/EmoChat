using Analys.api.contracts.BackgroundService;

namespace Analys.api.Implenemetation.BackgroundService
{
    public class MyTask : IScheduledTask
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            int i = 0;
            Console.WriteLine($"{DateTime.Now} And i = {i}");

        }
    }
}
