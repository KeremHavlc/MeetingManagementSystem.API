using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand
{
    public class DeleteDecisionAssignmentCommandHandler : IRequestHandler<DeleteDecisionAssignmentCommand, MessageResponse>
    {
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;

        public DeleteDecisionAssignmentCommandHandler(IDecisionAssignmentRepository decisionAssignmentRepository)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
        }

        public async Task<MessageResponse> Handle(DeleteDecisionAssignmentCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionAssignmentId , out Guid decisionAssignmentId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionAssignmentId Formatı!",
                    Success = false
                };
            }
            var existDecisionAssignment = await _decisionAssignmentRepository.GetWhereAsync(da => da.Id == decisionAssignmentId);

            if (existDecisionAssignment == null || !existDecisionAssignment.Any())
            {
                return new MessageResponse
                {
                    Message = "Karar ataması bulunamadı!",
                    Success = false
                };
            }

            await _decisionAssignmentRepository.RemoveAsync(decisionAssignmentId);

            return new MessageResponse
            {
                Message = "Karar ataması başarıyla silindi!",
                Success = true
            };
        }
    }
}
