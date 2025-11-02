using FluentValidation;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ChangePasswordUsingTokenCommand
{
    public class ChangePasswordUsingTokenCommandValidator : AbstractValidator<ChangePasswordUsingTokenCommand>
    {
        public ChangePasswordUsingTokenCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Doğrulama token'ı boş olamaz.");
        }
    }
}
