using MediatR;
using MeetingManagementSystem.Application.Features.UserFeatures.Commands.UpdateUserCommand;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserIdByUsernameOrEmailQuery;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserInfoQuery;
using MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserNameByIdQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserNameByUserId (GetUserNameByIdQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserIdByUsernameOrEmail(GetUserIdByUsernameOrEmailQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserInfo (GetUserInfoQuery request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
