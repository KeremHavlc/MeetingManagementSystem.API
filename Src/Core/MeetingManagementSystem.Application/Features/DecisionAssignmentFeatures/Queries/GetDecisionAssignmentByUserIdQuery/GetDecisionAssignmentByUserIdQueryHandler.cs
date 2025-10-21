using MediatR;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByUserIdQuery
{
    public class GetDecisionAssignmentByUserIdQueryHandler : IRequestHandler<GetDecisionAssignmentByUserIdQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;
        public GetDecisionAssignmentByUserIdQueryHandler(UserManager<AppUser> userManager, IDecisionAssignmentRepository decisionAssignmentRepository)
        {
            _userManager = userManager;
            _decisionAssignmentRepository = decisionAssignmentRepository;
        }

        public async Task<MessageResponse> Handle(GetDecisionAssignmentByUserIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "hatalı userId formatı!",
                    Success = false
                };
            }
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if(existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }
            var decisionAssignmentByUserId = await _decisionAssignmentRepository.GetWhereAsync(di => di.UserId == userId);
            if(decisionAssignmentByUserId == null || !decisionAssignmentByUserId.Any())
            {
                return new MessageResponse
                {
                    Message = "Karar ataması bu kullanıcı için bulunamadı!",
                    Success = false
                };
            }
            var result = decisionAssignmentByUserId.Select(x => new DecisionAssignmentDto
            {
                DecisionId = x.DecisionId.ToString(),
                UserId = x.UserId.ToString(),
                DecisionStatusEnum = x.DecisionStatus.ToString()
            });
            return new MessageResponse
            {
                Message = "Kullanıcının karar atamaları başarıyla bulundu!",
                Success = true,
                Data = result
            };
        }
    }
}
