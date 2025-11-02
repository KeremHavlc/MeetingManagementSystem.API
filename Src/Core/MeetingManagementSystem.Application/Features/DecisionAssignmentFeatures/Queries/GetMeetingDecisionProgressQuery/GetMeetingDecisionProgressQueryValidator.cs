using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetMeetingDecisionProgressQuery
{
    public class GetMeetingDecisionProgressQueryValidator : AbstractValidator<GetMeetingDecisionProgressQuery>
    {
        public GetMeetingDecisionProgressQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
