using Analys.api.Dto.UserEmo;
using Analys.api.Features.analysisProcessor.Requests;
using Analys.api.Features.UserEmo.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Analys.api.Controllers.user_emo
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEmoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserEmoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send/message")]
        public async Task<ActionResult> SendMessage(SendMessage_D sendMessage_D)
        {
            var command = new SendMessage_R { SendMessage_D = sendMessage_D };
            var response = await _mediator.Send(command);
            
            return StatusCode(statusCode: response.StatusCode, response);
        }

        [HttpGet("send/message")]
        public async Task<ActionResult> syncdateredis_mysql()
        {
            var command = new SyncUserEmojisFromRedis_R {  };
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }
    }
}
