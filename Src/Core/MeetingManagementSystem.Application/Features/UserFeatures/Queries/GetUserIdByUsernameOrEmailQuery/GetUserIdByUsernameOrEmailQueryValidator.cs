using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserIdByUsernameOrEmailQuery
{
    public class GetUserIdByUsernameOrEmailQueryValidator : AbstractValidator<GetUserIdByUsernameOrEmailQuery>
    {
        public GetUserIdByUsernameOrEmailQueryValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty().WithMessage("Kullanıcı adı veya e-posta boş olamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı veya e-posta en az 3 karakter olmalıdır.");
        }
    }
}
