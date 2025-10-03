using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery
{
    public class GetMeetingByIdQueryHandler : IRequestHandler<GetMeetingByIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;

        public GetMeetingByIdQueryHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if(meeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Toplantı başarıyla bulundu!",
                Success = true,
                Data = meeting
            };
        }
    }
}
