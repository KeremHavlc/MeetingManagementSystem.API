using MediatR;
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
    }
}
