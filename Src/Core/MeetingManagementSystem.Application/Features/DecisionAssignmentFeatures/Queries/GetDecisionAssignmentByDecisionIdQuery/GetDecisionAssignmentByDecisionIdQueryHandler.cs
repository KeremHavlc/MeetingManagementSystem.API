using MediatR;
using MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByDecisionIdQuery
{
    public class GetDecisionAssignmentByDecisionIdQueryHandler : IRequestHandler<GetDecisionAssignmentByDecisionIdQuery, MessageResponse>
    {
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;
        private readonly IDecisionRepository _decisionRepository;

        public GetDecisionAssignmentByDecisionIdQueryHandler(IDecisionAssignmentRepository decisionAssignmentRepository, IDecisionRepository decisionRepository)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(GetDecisionAssignmentByDecisionIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionId , out Guid decisionId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionId formatı!",
                    Success = false
                };
            }

            var existDecision = await _decisionRepository.GetSingleAsync(di => di.Id == decisionId);
            if(existDecision == null)
            {
                return new MessageResponse
                {
                    Message = "Karar bulunamadı!",
                    Success = false
                };
            }
            var decisionAssignmentByDecisionId = await _decisionAssignmentRepository.GetWhereAsync(di => di.DecisionId == existDecision.Id);
            if(decisionAssignmentByDecisionId == null || !decisionAssignmentByDecisionId.Any())
            {
                return new MessageResponse
                {
                    Message = "Karar ataması bulunamadı!",
                    Success = false
                };
            }
            var result = decisionAssignmentByDecisionId.Select(x => new DecisionAssignmentDto
            {
                DecisionId = x.DecisionId.ToString(),
                UserId = x.UserId.ToString(),
                DecisionStatusEnum = x.DecisionStatus.ToString()
            });
            return new MessageResponse
            {
                Message = "Karar atamaları başarıyla bulundu!",
                Success = true,
                Data = result
            };
        }
    }
}
