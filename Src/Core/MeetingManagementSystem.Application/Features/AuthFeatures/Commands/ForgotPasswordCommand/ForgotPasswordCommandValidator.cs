using FluentValidation;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty().WithMessage("Kullanıcı adı veya e-posta adresi boş olamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı veya e-posta en az 3 karakter olmalıdır.");
        }
    }
}
