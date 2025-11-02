using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DashboardFeatures.Queries.GetUpcomingMeetingsQuery
{
    public class GetUpcomingMeetingsQueryValidator : AbstractValidator<GetUpcomingMeetingsQuery>
    {
        public GetUpcomingMeetingsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
