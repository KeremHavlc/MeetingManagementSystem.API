using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingInviteFeatures.Commands
{
    public class CreateMeetingInviteCommandValidator : AbstractValidator<CreateMeetingInviteCommand>
    {
        public CreateMeetingInviteCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
