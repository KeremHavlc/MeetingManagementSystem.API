using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand
{
    public class DeleteMeetingParticipantCommandHandler : IRequestHandler<DeleteMeetingParticipantCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly UserManager<AppUser> _userManager;

        public DeleteMeetingParticipantCommandHandler(IMeetingRepository meetingRepository, UserManager<AppUser> userManager)
        {
            _meetingRepository = meetingRepository;
            _userManager = userManager;
        }



        public async Task<MessageResponse> Handle(DeleteMeetingParticipantCommand request, CancellationToken cancellationToken)
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
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }
            throw new Exception();
        }
    }
}
