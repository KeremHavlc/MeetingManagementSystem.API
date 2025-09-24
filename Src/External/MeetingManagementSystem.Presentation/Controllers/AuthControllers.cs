using MediatR;
using MeetingManagementSystem.Presentation.Abstraction;

namespace MeetingManagementSystem.Presentation.Controllers
{
    public class AuthControllers : ApiController
    {
        public AuthControllers(IMediator mediator) : base(mediator)
        {
        }
    }
}
