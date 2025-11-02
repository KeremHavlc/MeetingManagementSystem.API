using FluentValidation;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Queries.GetMessagesByMeetingIdQuery
{
    public class GetMessagesByMeetingIdQueryValidator : AbstractValidator<GetMessagesByMeetingIdQuery>
    {
        public GetMessagesByMeetingIdQueryValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Sayfa boyutu 1 ile 200 arasında olmalıdır.");
        }
    }
}
