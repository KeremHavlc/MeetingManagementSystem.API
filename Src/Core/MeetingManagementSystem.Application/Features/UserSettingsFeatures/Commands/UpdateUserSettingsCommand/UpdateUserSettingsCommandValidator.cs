using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.UpdateUserSettingsCommand
{
    public class UpdateUserSettingsCommandValidator : AbstractValidator<UpdateUserSettingsCommand>
    {
        public UpdateUserSettingsCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

            RuleFor(x => x.ReceiveMeetingJoinNotifications)
                .NotNull().WithMessage("Toplantı katılım bildirimi tercihi belirtilmelidir.");

            RuleFor(x => x.ReceiveDecisionNotifications)
                .NotNull().WithMessage("Karar bildirimi tercihi belirtilmelidir.");
        }
    }
}
