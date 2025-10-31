using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Enums;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetDashboardStatsQuery
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IDecisionRepository _decisionRepository;
        private readonly IDecisionAssignmentRepository _decisionAssignmentRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;

        public GetDashboardStatsQueryHandler(IDecisionAssignmentRepository decisionAssignmentRepository, IDecisionRepository decisionRepository, IMeetingRepository meetingRepository, UserManager<AppUser> userManager, IMeetingParticipantRepository meetingParticipantRepository)
        {
            _decisionAssignmentRepository = decisionAssignmentRepository;
            _decisionRepository = decisionRepository;
            _meetingRepository = meetingRepository;
            _userManager = userManager;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.UserId , out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }
            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if(existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı Bulunamadı!",
                    Success = false
                };
            }

            var createdMeetings = await _meetingRepository.GetWhereAsync(m => m.CreatedByUserId == userId);

            var participantMeetings = await _meetingParticipantRepository.GetWhereAsync(p => p.UserId == userId);

            var meetingIds = createdMeetings
                .Select(m => m.Id)
                .Union(participantMeetings.Select(p => p.MeetingId))
                .Distinct()
                .ToList();

            var decisions = meetingIds.Count == 0
                ? new List<Decision>()
                : await _decisionRepository.GetWhereAsync(d => meetingIds.Contains(d.MeetingId));

            var assignments = await _decisionAssignmentRepository.GetWhereAsync(a => a.UserId == userId);

            int totalMeetings = meetingIds.Count;
            int totalDecisions = decisions.Count;

            int activeAssignments = assignments.Count(a =>
                a.DecisionStatus == DecisionStatusEnum.Pending ||
                a.DecisionStatus == DecisionStatusEnum.InProgress);

            int completedCount = assignments.Count(a => a.DecisionStatus == DecisionStatusEnum.Completed);

            double completionRate = assignments.Any()
                ? Math.Round(completedCount * 100.0 / assignments.Count, 1)
                : 0;

            var dto = new
            {
                TotalMeetings = totalMeetings,
                TotalDecisions = totalDecisions,
                ActiveAssignments = activeAssignments,
                CompletionRate = completionRate
            };

            return new MessageResponse
            {
                Message = "Dashboard istatistikleri başarıyla getirildi!",
                Success = true,
                Data = dto
            };
        }
    }
}
