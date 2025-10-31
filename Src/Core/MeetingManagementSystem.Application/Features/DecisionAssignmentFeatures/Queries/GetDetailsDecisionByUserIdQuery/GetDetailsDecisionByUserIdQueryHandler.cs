using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDetailsDecisionByUserIdQuery
{
    public class GetDetailsDecisionByUserIdQueryHandler : IRequestHandler<GetDetailsDecisionByUserIdQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;
        private readonly IMeetingRepository _meetingRepository;

        public GetDetailsDecisionByUserIdQueryHandler(UserManager<AppUser> userManager, IMeetingRepository meetingRepository, IDecisionAssignmentRepository decisionAssignmentRepository, IDecisionRepository decisionRepository)
        {
            _userManager = userManager;
            _meetingRepository = meetingRepository;
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(GetDetailsDecisionByUserIdQuery request, CancellationToken cancellationToken)
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

            var assignments = await _decisionAssignmentRepository.GetWhereAsync(da => da.UserId == userId);
            if (assignments == null || !assignments.Any())
            {
                return new MessageResponse
                {
                    Message = "Kullanıcıya ait karar ataması bulunamadı!",
                    Success = false
                };
            }

            var detailsList = new List<object>();

            foreach (var assignment in assignments)
            {
                var decision = await _decisionRepository.GetByIdAsync(assignment.DecisionId);
                if (decision is null)
                    continue;

                var meeting = await _meetingRepository.GetByIdAsync(decision.MeetingId);

                var detail = new
                {
                    DecisionAssignmentId = assignment.Id,
                    DecisionId = decision.Id,
                    DecisionTitle = decision.Title,
                    DecisionDescription = decision.Description,
                    DecisionStatusEnum = assignment.DecisionStatus.ToString(),
                    DecisionCreatedAt = decision.CreatedAt,
                    MeetingId = meeting?.Id,
                    MeetingTitle = meeting?.Title,
                    MeetingDate = meeting?.ScheduledAt,
                    CreatedByUserId = meeting?.CreatedByUserId
                };

                detailsList.Add(detail);
            }

            return new MessageResponse
            {
                Message = "Kullanıcının karar detayları başarıyla bulundu!",
                Success = true,
                Data = detailsList
            };


        }
    }
}
