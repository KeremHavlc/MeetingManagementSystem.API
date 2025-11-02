using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetMeetingStatisticsQuery
{
    public class GetMeetingStatisticsQueryValidator : AbstractValidator<GetMeetingStatisticsQuery>
    {
        public GetMeetingStatisticsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.Period)
                .NotEmpty().WithMessage("İstatistik dönemi boş olamaz.")
                .Must(p => p == "day" || p == "week" || p == "month" || p == "year")
                .WithMessage("Geçerli bir istatistik dönemi belirtin (day, week, month, year).");
        }
    }
}
