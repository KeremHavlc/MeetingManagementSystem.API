using MediatR;
using MeetingManagementSystem.Application.Features.UserFeatures.Commands.UpdateUserCommand;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserIdByUsernameOrEmailQuery;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserInfoQuery;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserNameByIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("GetUserNameByUserId")]
        public async Task<IActionResult> GetUserNameByUserId([FromBody] GetUserNameByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetUserIdByUsernameOrEmail")]
        public async Task<IActionResult> GetUserIdByUsernameOrEmail([FromBody] GetUserIdByUsernameOrEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
