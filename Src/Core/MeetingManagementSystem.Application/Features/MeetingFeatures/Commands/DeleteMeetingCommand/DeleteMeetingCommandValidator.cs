using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.DeleteMeetingCommand
{
    public class DeleteMeetingCommandValidator : AbstractValidator<DeleteMeetingCommand>
    {
        public DeleteMeetingCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
