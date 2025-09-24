using MediatR;
using MeetingManagementSystem.Application.Features.AuthFeatures.LoginCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.RegisterCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.SignInCommand;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class AuthControllers : ApiController
    {
        public AuthControllers(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInCommand request , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
