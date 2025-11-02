using FluentValidation;

namespace MeetingManagementSystem.Application.Features.DecisionFeatures.Commands.CreateDecisionCommand
{
    public class CreateDecisionCommandValidator : AbstractValidator<CreateDecisionCommand>
    {
        public CreateDecisionCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Karar başlığı boş olamaz.")
                .MaximumLength(200).WithMessage("Karar başlığı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Karar açıklaması boş olamaz.")
                .MaximumLength(2000).WithMessage("Karar açıklaması en fazla 2000 karakter olabilir.");
        }
    }
}
