using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Features.DecisionFeatures.Dtos;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionsByMeetingIdQuery
{
    public class GetDecisionsByMeetingIdQueryHandler : IRequestHandler<GetDecisionsByMeetingIdQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IDecisionRepository _decisionRepository;

        public GetDecisionsByMeetingIdQueryHandler(IDecisionRepository decisionRepository, IMeetingRepository meetingRepository)
        {
            _decisionRepository = decisionRepository;
            _meetingRepository = meetingRepository;
        }

        public async Task<MessageResponse> Handle(GetDecisionsByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.MeetingId , out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }
            var existMeeting = await _meetingRepository.GetSingleAsync(mi => mi.Id == meetingId);
            if(existMeeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }
            var decisionByMeetingId = await _decisionRepository.GetWhereAsync(dcs => dcs.MeetingId == meetingId);
            if(decisionByMeetingId == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantıya ait karar bulunamadı!",
                    Success = false
                };
            }
            var decisionDto = decisionByMeetingId.Adapt<List<DecisionDto>>();
            return new MessageResponse
            {
                Message = "Kararlar başarıyla bulundu!",
                Success = true,
                Data = decisionDto
            };
        }
    }
}
