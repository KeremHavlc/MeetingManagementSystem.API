using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionByIdQuery
{
    public class GetDecisionByIdQueryHandler : IRequestHandler<GetDecisionByIdQuery, MessageResponse>
    {
        private readonly IDecisionRepository _decisionRepository;

        public GetDecisionByIdQueryHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(GetDecisionByIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionId , out Guid decisionId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionId formatı!",
                    Success = false
                };
            }
            var existDecision = await _decisionRepository.GetWhereAsync(di => di.Id == decisionId);

            if(existDecision == null)
            {
                return new MessageResponse
                {
                    Message = "Karar bulunamadı!",
                    Success = false
                };
            }

            return new MessageResponse
            {
                Message = "Karar detayı başarıyla bulundu!",
                Success = true,
                Data = existDecision
            };
        }
    }
}
