using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.UpdateDecisionCommand
{
    public class UpdateDecisionCommandValidator : AbstractValidator<UpdateDecisionCommand>
    {
        public UpdateDecisionCommandValidator()
        {
            RuleFor(x => x.DecisionId)
                .NotEmpty().WithMessage("Karar ID boş olamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Karar başlığı boş olamaz.")
                .MaximumLength(200).WithMessage("Karar başlığı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Karar açıklaması boş olamaz.")
                .MaximumLength(2000).WithMessage("Karar açıklaması en fazla 2000 karakter olabilir.");
        }
    }
}
