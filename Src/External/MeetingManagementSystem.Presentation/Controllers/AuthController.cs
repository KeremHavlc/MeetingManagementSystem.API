using MediatR;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.SignInCommand;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterCommand request , CancellationToken cancellationToken)
        {
            MessageResponse result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand request , CancellationToken cancellationToken)
        {
            MessageResponse result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInCommand request , CancellationToken cancellationToken)
        {
            MessageResponse result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
