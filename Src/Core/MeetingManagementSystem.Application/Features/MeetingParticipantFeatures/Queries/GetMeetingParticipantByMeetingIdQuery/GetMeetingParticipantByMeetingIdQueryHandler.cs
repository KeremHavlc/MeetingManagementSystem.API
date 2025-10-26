using MediatR;
using MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByMeetingIdQuery
{
    public class GetMeetingParticipantByMeetingIdQueryHandler : IRequestHandler<GetMeetingParticipantByMeetingIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;

        public GetMeetingParticipantByMeetingIdQueryHandler(IMeetingParticipantRepository meetingParticipantRepository, IMeetingRepository meetingRepository)
        {
            _meetingParticipantRepository = meetingParticipantRepository;
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingParticipantByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.MeetingId , out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetSingleAsync(me => me.Id == meetingId);
            if(existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı Bulunamadı!",
                    Success = false
                };
            }
            var meetingParticipant = await _meetingParticipantRepository.GetWhereAsync(mp => mp.MeetingId == meetingId);
            if (meetingParticipant == null || !meetingParticipant.Any())
            {
                return new MessageResponse
                {
                    Message = "Toplantı Katılımcısı bulunmamaktadır",
                    Success = false
                };
            }
            var participantDtos = meetingParticipant.Select(mp => new MeetingParticipantDto
            {
                UserId = mp.UserId,
                RoleId = mp.RoleId
            }).ToList();

            return new MessageResponse
            {
                Message = "Toplantı katılımcıları başarıyla bulunmuştur!",
                Success = true,
                Data = participantDtos
            };
        }
    }
}
