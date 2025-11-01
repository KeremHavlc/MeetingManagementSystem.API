using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.UpdateDecisionAssignmentStatusCommand
{
    public class UpdateDecisionAssignmentStatusCommandHandler : IRequestHandler<UpdateDecisionAssignmentStatusCommand, MessageResponse>
    {
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;

        public UpdateDecisionAssignmentStatusCommandHandler(IDecisionAssignmentRepository decisionAssignmentRepository)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
        }

        public async Task<MessageResponse> Handle(UpdateDecisionAssignmentStatusCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionAssignmentId , out Guid decisionAssignmentId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionAssignmentId formatı!",
                    Success = false
                };
            }

            var existDecisionAssignment = await _decisionAssignmentRepository.GetSingleAsync(da => da.Id == decisionAssignmentId);
            if(existDecisionAssignment == null)
            {
                return new MessageResponse
                {
                    Message = "Karar ataması bulunamadı!",
                    Success = false
                };
            }
            existDecisionAssignment.DecisionStatus = request.DecisionStatusEnum;
            await _decisionAssignmentRepository.UpdateAsync(existDecisionAssignment);
            return new MessageResponse
            {
                Message = "Karar durumu başarıyla değiştirildi!",
                Success = true
            };

        }
    }
}
