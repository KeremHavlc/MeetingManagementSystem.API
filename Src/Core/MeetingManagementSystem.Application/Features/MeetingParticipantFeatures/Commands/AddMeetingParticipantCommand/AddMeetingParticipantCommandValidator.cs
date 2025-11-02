using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.AddMeetingParticipantCommand
{
    public class AddMeetingParticipantCommandValidator : AbstractValidator<AddMeetingParticipantCommand>
    {
        public AddMeetingParticipantCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
