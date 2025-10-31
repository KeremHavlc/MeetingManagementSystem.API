using MediatR;
using MeetingManagementSystem.Application.Features.MeetingFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery
{
    public class GetMeetingsByUserIdQueryHandler : IRequestHandler<GetMeetingsByUserIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        public GetMeetingsByUserIdQueryHandler(IMeetingRepository meetingRepository, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }
            var participantMeetings = await _meetingParticipantRepository.GetWhereAsync(mp => mp.UserId == userId);
            var participantMeetingIds = participantMeetings.Select(mp => mp.MeetingId).Distinct().ToList();

            var createdMeetings = await _meetingRepository.GetWhereAsync(m => m.CreatedByUserId == userId);

            var allMeetingIds = createdMeetings.Select(m => m.Id)
                .Concat(participantMeetingIds)
                .Distinct()
                .ToList();

            var meetings = await _meetingRepository.GetWhereAsync(m => allMeetingIds.Contains(m.Id));

            if (meetings == null || !meetings.Any())
            {
                return new MessageResponse
                {
                    Message = "Kullanıcının bulunduğu toplantı bulunamadı!",
                    Success = true,
                    Data = new List<MeetingSummaryDto>()
                };
            }

            var meetingDtos = meetings.Select(m => new MeetingSummaryDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledAt = m.ScheduledAt,
                CreatedByUserId = m.CreatedByUserId,
                IsActive = m.IsActive,
                CreatedAt = m.CreatedAt
            }).ToList();

            return new MessageResponse
            {
                Message = "Kullanıcının bulunduğu toplantılar başarıyla getirildi!",
                Success = true,
                Data = meetingDtos
            };
        }
    }
}
