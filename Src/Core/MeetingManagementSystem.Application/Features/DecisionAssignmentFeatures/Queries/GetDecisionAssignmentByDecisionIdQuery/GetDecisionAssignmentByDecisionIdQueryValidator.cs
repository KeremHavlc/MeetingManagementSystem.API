using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByDecisionIdQuery
{
    public class GetDecisionAssignmentByDecisionIdQueryValidator : AbstractValidator<GetDecisionAssignmentByDecisionIdQuery>
    {
        public GetDecisionAssignmentByDecisionIdQueryValidator()
        {
            RuleFor(x => x.DecisionId)
                .NotEmpty().WithMessage("Karar ID boş olamaz.");
        }
    }
}
