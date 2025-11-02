using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserInfoQuery
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
