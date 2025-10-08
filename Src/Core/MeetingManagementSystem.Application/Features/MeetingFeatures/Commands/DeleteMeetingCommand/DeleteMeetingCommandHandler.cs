using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.DeleteMeetingCommand
{
    public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;

        public DeleteMeetingCommandHandler(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
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

            await _meetingRepository.RemoveAsync(meetingId);
            return new MessageResponse
            {
                Message = "Toplantı silindi!",
                Success = true
            };
        }
    }
}
