using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.CreateDecisionCommand
{
    public class CreateDecisionCommandHandler : IRequestHandler<CreateDecisionCommand, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IDecisionRepository _decisionRepository;

        public CreateDecisionCommandHandler(IDecisionRepository decisionRepository, IMeetingRepository meetingRepository)
        {
            _decisionRepository = decisionRepository;
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(CreateDecisionCommand request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetByIdAsync(meetingId);
            if(existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }
            var decision = request.Adapt<Decision>();
            await _decisionRepository.AddAsync(decision);
            return new MessageResponse
            {
                Message = "Karar başarıyla oluşturuldu!",
                Success = true,
                Data = new
                {
                   decision.MeetingId,
                   decision.Title,
                   decision.Description
                }
            };
        }
    }
}
