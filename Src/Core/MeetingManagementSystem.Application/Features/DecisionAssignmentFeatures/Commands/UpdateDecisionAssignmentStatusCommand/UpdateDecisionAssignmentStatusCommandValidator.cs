using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.UpdateDecisionAssignmentStatusCommand
{
    public class UpdateDecisionAssignmentStatusCommandValidator : AbstractValidator<UpdateDecisionAssignmentStatusCommand>
    {
        public UpdateDecisionAssignmentStatusCommandValidator()
        {
            RuleFor(x => x.DecisionAssignmentId)
                .NotEmpty().WithMessage("Karar atama ID'si boş olamaz.");

            RuleFor(x => x.DecisionStatusEnum)
                .IsInEnum().WithMessage("Geçerli bir karar durumu seçiniz.");
        }
    }
}
