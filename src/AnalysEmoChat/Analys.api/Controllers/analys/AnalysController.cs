using Analys.api.Features.analysisProcessor.Handlers;
using Analys.api.Features.analysisProcessor.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Analys.api.Controllers.analys
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AnalysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Get

        [HttpGet("get/second/page/top/emoji_for_user/{user_id}/{pagesize}")]
        public async Task<ActionResult> GetSecondPageTopEmojiForUser(int user_id , int pagesize)
        {
            var command = new GetSecondPageTopEmojiForUser_R 
                {pagesize = pagesize , user_id = user_id };
            var response = await _mediator.Send(command);

            return StatusCode(statusCode:response.StatusCode,response);
        }

        [HttpGet("get/second/page/top/user_emoji_usage/{emoji}/{pagesize}")]
        public async Task<ActionResult> GetSecondPageTopUserEmojiUsage(string emoji , int pagesize)
        {
            var command = new GetSecondPageTopUserEmojiUsage_R { emoji = emoji , pagesize = pagesize };
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }

     
        [HttpGet("get/top/{top}/emjo_for_user/{user_id}")]
        public async Task<ActionResult> GetTopEmojiForUser(int top, int user_id)
        { 
            var command = new GetTopEmojiForUser_R { top = top, user_id = user_id }; ;
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }


        [HttpGet("get/top/{top}/user_emoji_usage/{user_id}")]
        public async Task<ActionResult> GetTopUserEmojiUsage(int top, int user_id)
        {
            var command = new GetTopUserEmojiUsage_R { top = top , user_id =user_id };
            var response = await _mediator.Send(command);

            return StatusCode(statusCode: response.StatusCode, response);
        }
        #endregion

    }
}
