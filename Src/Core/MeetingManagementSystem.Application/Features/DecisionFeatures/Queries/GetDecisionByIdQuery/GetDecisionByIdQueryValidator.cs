using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Queries.GetDecisionByIdQuery
{
    public class GetDecisionByIdQueryValidator : AbstractValidator<GetDecisionByIdQuery>
    {
        public GetDecisionByIdQueryValidator()
        {
            RuleFor(x => x.DecisionId)
                .NotEmpty().WithMessage("Karar ID boş olamaz.");
        }
    }
}
