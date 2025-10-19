using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.DeleteDecisionCommand
{
    public class DeleteDecisionCommandHandler : IRequestHandler<DeleteDecisionCommand, MessageResponse>
    {
        private readonly IDecisionRepository _decisionRepository;

        public DeleteDecisionCommandHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(DeleteDecisionCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionId , out Guid decisionId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionId Formatı!",
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
            await _decisionRepository.RemoveAsync(decisionId);
            return new MessageResponse
            {
                Message = "Karar başarıyla silindi!",
                Success = true
            };
        }
    }
}
