using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionsByMeetingIdQuery
{
    public class GetDecisionsByMeetingIdQueryValidator : AbstractValidator<GetDecisionsByMeetingIdQuery>
    {
        public GetDecisionsByMeetingIdQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
