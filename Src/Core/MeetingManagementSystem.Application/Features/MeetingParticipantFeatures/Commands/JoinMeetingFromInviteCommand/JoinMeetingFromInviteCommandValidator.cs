using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.JoinMeetingFromInviteCommand
{
    public class JoinMeetingFromInviteCommandValidator : AbstractValidator<JoinMeetingFromInviteCommand>
    {
        public JoinMeetingFromInviteCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
