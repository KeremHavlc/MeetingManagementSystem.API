using MediatR;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Presentation.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Controller
{
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterCommand request , CancellationToken cancellationToken)
        {
            MessageResponseDto result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand request , CancellationToken cancellationToken)
        {
            MessageResponseDto result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

    }
}
