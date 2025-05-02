using Analys.api.contracts.BackgroundService;
using Analys.api.contracts.Repository.redis;
using Analys.api.Features.UserEmo.Requests;
using Analys.api.model.user;
using MediatR;

namespace Analys.api.Implenemetation.BackgroundService
{
    public class TransferRedisEmojiToMySqlTask : IScheduledTask
    {
        private readonly IMediator _mediator;

        public TransferRedisEmojiToMySqlTask( IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("process transfer data from redis to mysql STARTED ...");

            var request = new SyncUserEmojisFromRedis_R() { };

            var response = await _mediator.Send(request, cancellationToken);

            Console.WriteLine($"status transfer data:{response.IsSuccess}");
            Console.WriteLine(response.Message);
        }
    }

}
