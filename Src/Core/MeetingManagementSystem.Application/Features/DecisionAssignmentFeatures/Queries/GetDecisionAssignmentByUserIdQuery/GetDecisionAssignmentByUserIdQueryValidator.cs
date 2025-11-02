using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDecisionAssignmentByUserIdQuery
{
    public class GetDecisionAssignmentByUserIdQueryValidator : AbstractValidator<GetDecisionAssignmentByUserIdQuery>
    {
        public GetDecisionAssignmentByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
