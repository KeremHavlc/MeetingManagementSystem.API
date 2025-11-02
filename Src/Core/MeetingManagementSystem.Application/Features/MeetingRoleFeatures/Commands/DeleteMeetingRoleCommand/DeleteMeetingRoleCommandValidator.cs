using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.DeleteMeetingRoleCommand
{
    public class DeleteMeetingRoleCommandValidator : AbstractValidator<DeleteMeetingRoleCommand>
    {
        public DeleteMeetingRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Rol ID boş olamaz.");
        }
    }
}
