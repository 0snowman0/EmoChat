using ChatSystem_Application.Dto.Message.command;
using ChatSystem_Application.Features.message.Requests.Commands;
using ChatSystem_Application.Features.message.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace chatSystem_api.Controllers.message
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Add/Message")]
        public async Task<ActionResult> AddMessage(Message_C_D message_C_D)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            var command = new Message_C_R {Message_C_D = message_C_D };
            var response = await _mediator.Send(command);


            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            response.Message += $" | ExecutionTime: {elapsedMilliseconds} ms";

            return StatusCode(statusCode: response.StatusCode , response);
        }

        [HttpDelete("remove/message")]
        public async Task<ActionResult> DeleteMessage(string message_id)
        {
            var command = new Message_D_R { Id = message_id };
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }


        [HttpPut("update/message")]
        public async Task<ActionResult> UpdateMessage(Message_U_D message_U_D)
        {
            var command = new Message_U_R { Message_U_D = message_U_D};
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }

        //ToDo
        [HttpGet("Get/Message/{order}/{ReceiverID}/{senderID}")]
        public async Task<ActionResult> GetMessage(int order, int ReceiverID , int senderID)
        {
            var command = new Message_GA_R
            {
                order = order,
                ReceiverID = ReceiverID,
                senderID = senderID 
            };
            var response = await _mediator.Send(command);
               
            return StatusCode(statusCode: response.StatusCode, response);
        }
    }
}
