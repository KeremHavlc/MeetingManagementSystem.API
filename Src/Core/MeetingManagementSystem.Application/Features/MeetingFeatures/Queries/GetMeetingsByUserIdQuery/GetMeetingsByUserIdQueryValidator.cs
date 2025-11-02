using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingsByUserIdQuery
{
    public class GetMeetingsByUserIdQueryValidator : AbstractValidator<GetMeetingsByUserIdQuery>
    {
        public GetMeetingsByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
