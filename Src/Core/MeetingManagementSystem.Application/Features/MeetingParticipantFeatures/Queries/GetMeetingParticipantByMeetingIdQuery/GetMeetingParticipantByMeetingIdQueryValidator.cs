using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantByMeetingIdQuery
{
    public class GetMeetingParticipantByMeetingIdQueryValidator : AbstractValidator<GetMeetingParticipantByMeetingIdQuery>
    {
        public GetMeetingParticipantByMeetingIdQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
