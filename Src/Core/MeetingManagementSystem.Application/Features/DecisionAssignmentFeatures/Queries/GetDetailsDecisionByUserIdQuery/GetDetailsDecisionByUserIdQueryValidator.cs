using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Queries.GetDetailsDecisionByUserIdQuery
{
    public class GetDetailsDecisionByUserIdQueryValidator : AbstractValidator<GetDetailsDecisionByUserIdQuery>
    {
        public GetDetailsDecisionByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
