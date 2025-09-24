using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagementSystem.Presentation.Abstraction
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController
    {
        public readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
