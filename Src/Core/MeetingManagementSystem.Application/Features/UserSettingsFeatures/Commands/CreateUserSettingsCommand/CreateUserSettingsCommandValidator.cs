using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.CreateUserSettingsCommand
{
    public class CreateUserSettingsCommandValidator : AbstractValidator<CreateUserSettingsCommand>
    {
        public CreateUserSettingsCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
