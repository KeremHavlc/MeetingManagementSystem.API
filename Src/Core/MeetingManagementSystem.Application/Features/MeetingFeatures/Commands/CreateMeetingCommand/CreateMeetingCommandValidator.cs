using FluentValidation;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Commands.CreateMeetingCommand
{
    public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
    {
        public CreateMeetingCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Toplantı başlığı boş olamaz.")
                .MaximumLength(200).WithMessage("Toplantı başlığı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Toplantı açıklaması boş olamaz.")
                .MaximumLength(2000).WithMessage("Toplantı açıklaması en fazla 2000 karakter olabilir.");

            RuleFor(x => x.ScheduledAt)
                .NotEmpty().WithMessage("Toplantı tarihi boş olamaz.")
                .GreaterThan(DateTime.Now).WithMessage("Toplantı tarihi geçmiş bir tarih olamaz.");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty().WithMessage("Oluşturan kullanıcı ID boş olamaz.");
        }
    }
}
