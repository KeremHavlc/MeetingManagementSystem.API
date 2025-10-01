using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByNameRolesQuery
{
    public class GetByNameRolesQueryHandler : IRequestHandler<GetByNameRolesQuery, MessageResponse>
    {
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public GetByNameRolesQueryHandler(IMeetingRoleRepository meetingRoleRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
        }

        public async Task<MessageResponse> Handle(GetByNameRolesQuery request, CancellationToken cancellationToken)
        {
            var role = await _meetingRoleRepository.GetSingleAsync(mr => mr.RoleName == request.RoleName);
            if (role == null)
            {
                return new MessageResponse
                {
                    Message = "Role Bulunamadı!",
                    Success = false
                };
            }
            return new MessageResponse
            {
                Message = "Role başarıyla bulundu!",
                Success = true,
                Data = role
            };
        }
    }
}
