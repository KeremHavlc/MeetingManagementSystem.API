using FluentValidation;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ConfirmEmailCommand
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Doğrulama token'ı boş olamaz.")
                .MinimumLength(10).WithMessage("Token geçersiz görünüyor, lütfen geçerli bir bağlantı kullanın.");
        }
    }
}
