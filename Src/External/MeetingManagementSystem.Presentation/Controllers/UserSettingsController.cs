using MediatR;
using MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.CreateUserSettingsCommand;
using MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.UpdateUserSettingsCommand;
using MeetingManagementSystem.Application.Features.UserSettingsFeatures.Queries.GetUserSettingsQuery;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ApiController
    {
        public UserSettingsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateUserSettings")]
        public async Task<IActionResult> CreateUserSettings([FromBody] CreateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("UpdateUserSettings")]
        public async Task<IActionResult> UpdateUserSettings([FromBody] UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("GetUserSettings")]
        public async Task<IActionResult> GetUserSettings([FromBody] GetUserSettingsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
