using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery
{
    public class GetMeetingsByUserIdQueryHandler : IRequestHandler<GetMeetingsByUserIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;

        public GetMeetingsByUserIdQueryHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
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
            var meeting = await _meetingRepository.GetWhereAsync(mr => mr.CreatedByUserId == userId);
            if(meeting == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcının toplantıları bulunamadı!",
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
