using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using MeetingManagementSystem.Domain.Enums;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetMeetingDecisionProgressQuery
{
    public class GetMeetingDecisionProgressQueryHandler
        : IRequestHandler<GetMeetingDecisionProgressQuery, MessageResponse>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;

        public GetMeetingDecisionProgressQueryHandler(
            IMeetingRepository meetingRepository,
            IDecisionAssignmentRepository decisionAssignmentRepository,
            IDecisionRepository decisionRepository)
        {
            _meetingRepository = meetingRepository;
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingDecisionProgressQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.MeetingId, out Guid meetingId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı meetingId formatı!",
                    Success = false
                };
            }

            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                return new MessageResponse
                {
                    Message = "Toplantı bulunamadı!",
                    Success = false
                };
            }

            var decisions = await _decisionRepository.GetWhereAsync(d => d.MeetingId == meetingId);
            if (decisions == null || !decisions.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu toplantıya ait karar bulunamadı!",
                    Success = false
                };
            }

            var allAssignments = new List<DecisionAssignment>();
            foreach (var decision in decisions)
            {
                var assignments = await _decisionAssignmentRepository.GetWhereAsync(a => a.DecisionId == decision.Id);
                if (assignments != null)
                    allAssignments.AddRange(assignments);
            }

            if (!allAssignments.Any())
            {
                return new MessageResponse
                {
                    Message = "Bu toplantıya ait görev bulunamadı!",
                    Success = true,
                    Data = new
                    {
                        CompletionRate = 0,
                        Completed = 0,
                        Total = 0
                    }
                };
            }

            var completedCount = allAssignments.Count(a => a.DecisionStatus == DecisionStatusEnum.Completed);
            var totalCount = allAssignments.Count;

            double completionRate = Math.Round(((double)completedCount / totalCount) * 100, 1);

            return new MessageResponse
            {
                Message = "Toplantı karar ilerlemesi getirildi.",
                Success = true,
                Data = new
                {
                    CompletionRate = completionRate,
                    Completed = completedCount,
                    Total = totalCount
                }
            };
        }
    }
}
