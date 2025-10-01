using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByIdRolesQuery
{
    public class GetByIdRolesQueryHandler : IRequestHandler<GetByIdRolesQuery, MessageResponse>
    {
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public GetByIdRolesQueryHandler(IMeetingRoleRepository meetingRoleRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
        }

        public async Task<MessageResponse> Handle(GetByIdRolesQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.RoleId , out Guid roleId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı roleId formatı!",
                    Success = false
                };
            }
            var role = await _meetingRoleRepository.GetByIdAsync(roleId);
            if(role == null)
            {
                return new MessageResponse
                {
                    Message = "Rol bulunamadı!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Rol başarıyla bulundu!",
                Success = true,
                Data = role
            };
        }
    }
}
