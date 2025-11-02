using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserFeatures.Queries.GetUserNameByIdQuery
{
    public class GetUserNameByIdQueryValidator : AbstractValidator<GetUserNameByIdQuery>
    {
        public GetUserNameByIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
