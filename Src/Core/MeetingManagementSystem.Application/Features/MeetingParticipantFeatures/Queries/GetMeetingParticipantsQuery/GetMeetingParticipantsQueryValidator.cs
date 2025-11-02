using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingParticipantFeatures.Queries.GetMeetingParticipantsQuery
{
    public class GetMeetingParticipantsQueryValidator : AbstractValidator<GetMeetingParticipantsQuery>
    {
        public GetMeetingParticipantsQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");
        }
    }
}
