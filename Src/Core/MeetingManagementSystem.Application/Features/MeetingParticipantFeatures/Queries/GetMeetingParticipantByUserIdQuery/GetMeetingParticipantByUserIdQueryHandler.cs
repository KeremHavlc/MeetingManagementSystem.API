using MediatR;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByUserIdQuery
{
    public class GetMeetingParticipantByUserIdQueryHandler
        : IRequestHandler<GetMeetingParticipantByUserIdQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly IMeetingRepository _meetingRepository;

        public GetMeetingParticipantByUserIdQueryHandler(
            IMeetingParticipantRepository meetingParticipantRepository,
            UserManager<AppUser> userManager,
            IMeetingRepository meetingRepository)
        {
            _meetingParticipantRepository = meetingParticipantRepository;
            _userManager = userManager;
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingParticipantByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userGuid))
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

            var participantRecords = await _meetingParticipantRepository
                .GetWhereAsync(mp => mp.UserId == userGuid);

            if (participantRecords == null || !participantRecords.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu kullanıcı hiçbir toplantıya katılmamış.",
                    Success = false
                };
            }

            var meetingIds = participantRecords
                .Select(mp => mp.MeetingId)
                .Distinct()
                .ToList();

            var meetings = await _meetingRepository
                .GetWhereAsync(m => meetingIds.Contains(m.Id));

            var dtoList = meetings.Select(m => new MeetingDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledAt = m.ScheduledAt,
                CreatedByUserId = m.CreatedByUserId,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }).ToList();

            return new MessageResponse
            {
                Message = "Kullanıcının katıldığı toplantılar başarıyla getirildi!",
                Success = true,
                Data = dtoList
            };
        }
    }
}
