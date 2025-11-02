using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetDashboardStatsQuery
{
    public class GetDashboardStatsQueryValidator : AbstractValidator<GetDashboardStatsQuery>
    {
        public GetDashboardStatsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
