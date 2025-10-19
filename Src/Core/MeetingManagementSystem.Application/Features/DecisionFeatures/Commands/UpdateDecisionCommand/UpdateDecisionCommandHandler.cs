using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.UpdateDecisionCommand
{
    public class UpdateDecisionCommandHandler : IRequestHandler<UpdateDecisionCommand, MessageResponse>
    {
        private readonly IDecisionRepository _decisionRepository;

        public UpdateDecisionCommandHandler(IDecisionRepository decisionRepository)
        {
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(UpdateDecisionCommand request, CancellationToken cancellationToken)
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
            existDecision.Title = request.Title ?? existDecision.Title;
            existDecision.Description = request.Description ?? existDecision.Description;
            existDecision.UpdatedAt = DateTime.Now;

            await _decisionRepository.UpdateAsync(existDecision);

            return new MessageResponse
            {
                Message = "Karar başarıyla güncellendi!",
                Success = true
            };
        }
    }
}
