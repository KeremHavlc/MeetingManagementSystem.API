using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Commands.CreateMeetingRoleCommand
{
    public class CreateMeetingRoleCommandValidator : AbstractValidator<CreateMeetingRoleCommand>
    {
        public CreateMeetingRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Rol adı boş olamaz.")
                .MaximumLength(100).WithMessage("Rol adı en fazla 100 karakter olabilir.");
        }
    }
}
