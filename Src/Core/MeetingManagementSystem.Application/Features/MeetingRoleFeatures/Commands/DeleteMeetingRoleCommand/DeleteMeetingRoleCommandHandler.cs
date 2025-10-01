using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.DeleteMeetingRoleCommand
{
    public class DeleteMeetingRoleCommandHandler : IRequestHandler<DeleteMeetingRoleCommand, MessageResponse>
    {
        private readonly IMeetingRoleRepository _meetingRoleRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;
        public DeleteMeetingRoleCommandHandler(IMeetingRoleRepository meetingRoleRepository, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(DeleteMeetingRoleCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.RoleId , out Guid roleId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı roleId formatı!",
                    Success = false
                };
            }
            var role = await _meetingRoleRepository.GetSingleAsync(mr => mr.Id == roleId);
            if (role == null)
            {
                return new MessageResponse
                {
                    Message = "Role bulunamadı!",
                    Success = false
                };
            }
            var existRole = await _meetingParticipantRepository.GetWhereAsync(mr => mr.RoleId == roleId);
            if(existRole.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu rol kullanılmaktadır.",
                    Success = false
                };
            }
            await _meetingRoleRepository.RemoveAsync(roleId);
            return new MessageResponse
            {
                Message = "Rol başarıyla silindi!",
                Success = true
            };
        }
    }
}
