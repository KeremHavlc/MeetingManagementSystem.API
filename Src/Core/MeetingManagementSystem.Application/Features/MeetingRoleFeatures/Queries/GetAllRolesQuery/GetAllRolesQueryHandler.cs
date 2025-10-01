using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetAllRolesQuery
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, MessageResponse>
    {
        private readonly IMeetingRoleRepository _meetingRoleRepository;

        public GetAllRolesQueryHandler(IMeetingRoleRepository meetingRoleRepository)
        {
            _meetingRoleRepository = meetingRoleRepository;
        }

        public async Task<MessageResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _meetingRoleRepository.GetWhereAsync(ac => ac.IsActive == true);
            return new MessageResponse
            {
                Message = "Rol listesi başarıyla oluşturuldu!",
                Success = true,
                Data = roles.ToList()
            };
        }
    }
}
