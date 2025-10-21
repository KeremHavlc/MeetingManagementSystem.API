using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand
{
    public class CreateDecisionAssignmentCommandHandler : IRequestHandler<CreateDecisionAssignmentCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;

        public CreateDecisionAssignmentCommandHandler(IDecisionAssignmentRepository decisionAssignmentRepository, IDecisionRepository decisionRepository, UserManager<AppUser> userManager)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(CreateDecisionAssignmentCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.DecisionId , out Guid decisionId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı decisionId formatı!",
                    Success = false
                };
            }
            if(!Guid.TryParse(request.UserId , out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
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
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if(existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
                    Success = false
                };
            }
            var existDecisionAssignment = await _decisionAssignmentRepository.GetWhereAsync(da => da.DecisionId == decisionId && da.UserId == userId);

            if(existDecisionAssignment.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu user'a daha önceden decision atanmıştır!",
                    Success = false,
                };
            }

            var decisionAssignment = request.Adapt<DecisionAssignment>();

            await _decisionAssignmentRepository.AddAsync(decisionAssignment);

            return new MessageResponse
            {
                Message = "Karar ataması başarıyla tamamlandı!",
                Success = false
            };
        }
    }
}
