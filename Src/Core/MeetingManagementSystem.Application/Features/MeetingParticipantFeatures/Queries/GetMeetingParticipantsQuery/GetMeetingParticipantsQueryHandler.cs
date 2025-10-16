using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantsQuery
{
    public class GetMeetingParticipantsQueryHandler : IRequestHandler<GetMeetingParticipantsQuery, MessageResponse>
    {
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        private readonly IMeetingRepository _meetingRepository;

        public GetMeetingParticipantsQueryHandler(IMeetingRepository meetingRepository, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingParticipantsQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.MeetingId , out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Yanlış meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetByIdAsync(meetingId);
            if(existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }
            var meetingParticipant = await _meetingParticipantRepository.GetMeetingParticipantsWithUsersAsync(meetingId);
            if(meetingParticipant == null || !meetingParticipant.Any())
            {
                return new MessageResponse
                {
                    Message = "Toplantı da kullanıcı yoktur!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Kullanıcılar başarıyla bulundu!",
                Success = true,
                Data = meetingParticipant
            };
        }
    }
}
