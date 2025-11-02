using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery
{
    public class GetMeetingByIdQueryValidator : AbstractValidator<GetMeetingByIdQuery>
    {
        public GetMeetingByIdQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
