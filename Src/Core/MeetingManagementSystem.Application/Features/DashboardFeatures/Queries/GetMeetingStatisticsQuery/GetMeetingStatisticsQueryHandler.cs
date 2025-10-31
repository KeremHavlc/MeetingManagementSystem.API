using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetMeetingStatisticsQuery
{
    public class GetMeetingStatisticsQueryHandler : IRequestHandler<GetMeetingStatisticsQuery, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMeetingParticipantRepository _meetingParticipantRepository;

        public GetMeetingStatisticsQueryHandler(
            UserManager<AppUser> userManager,
            IMeetingRepository meetingRepository,
            IMeetingParticipantRepository meetingParticipantRepository)
        {
            _userManager = userManager;
            _meetingRepository = meetingRepository;
            _meetingParticipantRepository = meetingParticipantRepository;
        }

        public async Task<MessageResponse> Handle(GetMeetingStatisticsQuery request, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                return new MessageResponse
                {
                    Message = "Hatalı userId formatı!",
                    Success = false
                };
            }

            var existUser = await _userManager.FindByIdAsync(request.UserId);
            if (existUser == null)
            {
                return new MessageResponse
                {
                    Message = "Kullanıcı bulunamadı!",
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

            if (!meetingIds.Any())
            {
                return new MessageResponse
                {
                    Message = "Kullanıcının toplantısı bulunamadı!",
                    Success = true,
                    Data = new List<object>()
                };
            }

            var meetings = await _meetingRepository.GetWhereAsync(m => meetingIds.Contains(m.Id));

            List<object> result = new();

            switch (request.Period.ToLower())
            {
                case "week":
                    result = meetings
                        .Where(m => m.ScheduledAt >= DateTime.Now.AddDays(-7))
                        .GroupBy(m => m.ScheduledAt.Date)
                        .Select(g => new
                        {
                            Label = g.Key.ToString("dd MMM", CultureInfo.GetCultureInfo("tr-TR")),
                            Count = g.Count()
                        })
                        .OrderBy(g => g.Label)
                        .ToList<object>();
                    break;

                case "month":
                    result = meetings
                        .Where(m => m.ScheduledAt >= DateTime.Now.AddDays(-30))
                        .GroupBy(m => m.ScheduledAt.Date)
                        .Select(g => new
                        {
                            Label = g.Key.ToString("dd MMM", CultureInfo.GetCultureInfo("tr-TR")),
                            Count = g.Count()
                        })
                        .OrderBy(g => g.Label)
                        .ToList<object>();
                    break;

                case "year":
                default:
                    result = meetings
                        .Where(m => m.ScheduledAt.Year == DateTime.Now.Year)
                        .GroupBy(m => m.ScheduledAt.Month)
                        .Select(g => new
                        {
                            Label = CultureInfo.GetCultureInfo("tr-TR").DateTimeFormat.GetMonthName(g.Key),
                            Count = g.Count()
                        })
                        .OrderBy(g => g.Label)
                        .ToList<object>();
                    break;
            }

            return new MessageResponse
            {
                Message = "Toplantı istatistikleri başarıyla getirildi!",
                Success = true,
                Data = result
            };
        }
    }
}
