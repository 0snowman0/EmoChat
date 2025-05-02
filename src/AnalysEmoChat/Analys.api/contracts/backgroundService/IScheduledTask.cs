namespace Analys.api.contracts.BackgroundService
{
    public interface IScheduledTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
