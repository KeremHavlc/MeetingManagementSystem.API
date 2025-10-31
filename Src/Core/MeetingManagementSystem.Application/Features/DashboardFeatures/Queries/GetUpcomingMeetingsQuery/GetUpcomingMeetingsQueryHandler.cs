using MediatR;
using MeetingManagementSystem.Application.Features.DashboardFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetUpcomingMeetingsQuery
{
    public class GetUpcomingMeetingsQueryHandler : IRequestHandler<GetUpcomingMeetingsQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;

        public GetUpcomingMeetingsQueryHandler(
            UserManager<AppUser> userManager,
            IMeetingRepository meetingRepository,
            IMeetingParticipantRepository meetingParticipantRepository)
        {
            _userManager = userManager;
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(GetUpcomingMeetingsQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }

            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }

            var createdMeetings = await _meetingRepository.GetWhereAsync(m => m.CreatedByUserId == userId);

            var participantMeetings = await _meetingParticipantRepository.GetWhereAsync(p => p.UserId == userId);

            var meetingIds = createdMeetings
                .Select(m => m.Id)
                .Union(participantMeetings.Select(p => p.MeetingId))
                .Distinct()
                .ToList();

            var upcomingMeetings = meetingIds.Count == 0
                ? new List<Meeting>()
                : await _meetingRepository.GetWhereAsync(
                    m => meetingIds.Contains(m.Id) && m.ScheduledAt > DateTime.Now);

            var upcomingDtos = upcomingMeetings
                .OrderBy(m => m.ScheduledAt)
                .Take(5)
                .Select(m => new UpcomingMeetingDto
                {
                    Title = m.Title,
                    ScheduledAt = m.ScheduledAt
                })
                .ToList();

            return new MessageResponse
            {
                Message = "Yaklaşan toplantılar başarıyla getirildi!",
                Success = true,
                Data = upcomingDtos
            };
        }
    }
}
