using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand
{
    public class CreateMeetingRoleCommandHandler : IRequestHandler<CreateMeetingRoleCommand, MessageResponse>
    {
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public CreateMeetingRoleCommandHandler(IMeetingRoleRepository meetingRoleRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
        }

        /*
          Yapılacaklar
                        1->MeetingRole Crud (GetAll , GetById , GetByName , DeleteRole)
                        2->MessageResponse Data eklenmesi
                        3->CreateMeeting de otomatik admin rol ataması
                        4->CreateMeeting de otomatik MeetingParticipant oluşturulması
        */


        public async Task<MessageResponse> Handle(CreateMeetingRoleCommand request, CancellationToken cancellationToken)
        {
            var existRole = await _meetingRoleRepository.GetWhereAsync(mr => mr.RoleName == request.RoleName);
            if(existRole.Any())
            {
                return new MessageResponse
                {
                    Message = "Rol kullanılmaktadır.",
                    Success = false
                };
            }
            var role = request.Adapt<MeetingRole>();
            _meetingRoleRepository.Add(role);
            return new MessageResponse
            {
                Message = "Rol eklendi!",
                Success = true
            };
        }
    }
}
