using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.DeleteDecisionCommand
{
    public class DeleteDecisionCommandValidator : AbstractValidator<DeleteDecisionCommand>
    {
        public DeleteDecisionCommandValidator()
        {
            RuleFor(x => x.DecisionId)
                .NotEmpty().WithMessage("Karar ID boş olamaz.");
        }
    }
}
