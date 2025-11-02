using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Commands.DeleteMeetingParticipantCommand
{
    public class DeleteMeetingParticipantCommandValidator : AbstractValidator<DeleteMeetingParticipantCommand>
    {
        public DeleteMeetingParticipantCommandValidator()
        {
            RuleFor(x => x.DeleteUserId)
                .NotEmpty().WithMessage("Silinecek kullanıcının ID'si boş olamaz.");

            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
