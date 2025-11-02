using FluentValidation;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Commands.CreateChatMessageCommand
{
    public class CreateChatMessageCommandValidator : AbstractValidator<CreateChatMessageCommand>
    {
        public CreateChatMessageCommandValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty().WithMessage("Toplantı ID boş olamaz.");

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Gönderen kullanıcı ID boş olamaz.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj içeriği boş olamaz.")
                .MaximumLength(1000).WithMessage("Mesaj en fazla 1000 karakter olabilir.");
        }
    }
}
