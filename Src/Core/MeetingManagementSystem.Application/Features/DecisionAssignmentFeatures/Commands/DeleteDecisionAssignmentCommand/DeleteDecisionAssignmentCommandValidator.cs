using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.DeleteDecisionAssignmentCommand
{
    public class DeleteDecisionAssignmentCommandValidator : AbstractValidator<DeleteDecisionAssignmentCommand>
    {
        public DeleteDecisionAssignmentCommandValidator()
        {
            RuleFor(x => x.DecisionAssignmentId)
                .NotEmpty().WithMessage("Karar atama ID'si boş olamaz.");
        }
    }
}
