using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingRoleFeatures.Queries.GetByNameRolesQuery
{
    public class GetByNameRolesQueryValidator : AbstractValidator<GetByNameRolesQuery>
    {
        public GetByNameRolesQueryValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Rol adı boş olamaz.")
                .MaximumLength(100).WithMessage("Rol adı en fazla 100 karakter olabilir.");
        }
    }
}
