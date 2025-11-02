using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionAssignmentFeatures.Commands.CreateDecisionAssignmentCommand
{
    public class CreateDecisionAssignmentCommandValidator : AbstractValidator<CreateDecisionAssignmentCommand>
    {
        public CreateDecisionAssignmentCommandValidator()
        {
            RuleFor(x => x.DecisionId)
                .NotEmpty().WithMessage("Karar ID boş olamaz.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.DecisionStatus)
                .IsInEnum().WithMessage("Geçerli bir karar durumu seçiniz.");
        }
    }
}
