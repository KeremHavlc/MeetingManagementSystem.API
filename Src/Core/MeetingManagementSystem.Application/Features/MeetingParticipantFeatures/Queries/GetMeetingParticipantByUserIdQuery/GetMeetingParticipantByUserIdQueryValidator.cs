using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByUserIdQuery
{
    public class GetMeetingParticipantByUserIdQueryValidator : AbstractValidator<GetMeetingParticipantByUserIdQuery>
    {
        public GetMeetingParticipantByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");
        }
    }
}
