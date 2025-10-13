using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MeetingManagementSystem.Infrastructure.Security.Authorization
{
    public class MeetingRoleHandler : AuthorizationHandler<MeetingRoleRequirement>
    {
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeetingRoleHandler(IHttpContextAccessor httpContextAccessor, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MeetingRoleRequirement requirement)
        {
            //JWT'den token al
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("id").Value;
            if (userIdClaim == null) return;

            var userId = Guid.Parse(userIdClaim);

            //Routedan meetingId Al
            //var meetingId = _httpContextAccessor.HttpContext?.Request.RouteValues

        }
    }
}
