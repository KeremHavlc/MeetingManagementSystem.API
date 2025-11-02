using FluentValidation;

namespace MeetingManagementSystem.Application.Features.UserSettingsFeatures.Queries.GetUserSettingsQuery
{
    public class GetUserSettingsQueryValidator : AbstractValidator<GetUserSettingsQuery>
    {
        public GetUserSettingsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
