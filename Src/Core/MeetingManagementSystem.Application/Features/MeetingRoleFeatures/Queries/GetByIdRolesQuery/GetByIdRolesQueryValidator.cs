using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByIdRolesQuery
{
    public class GetByIdRolesQueryValidator : AbstractValidator<GetByIdRolesQuery>
    {
        public GetByIdRolesQueryValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Rol ID boş olamaz.");
        }
    }
}
