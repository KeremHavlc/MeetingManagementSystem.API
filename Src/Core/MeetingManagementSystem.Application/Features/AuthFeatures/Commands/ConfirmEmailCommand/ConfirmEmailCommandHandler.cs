using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MeetingManagementSystem.Application.Features.UserSettingsFeatures.Commands.CreateUserSettingsCommand;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.ConfirmEmailCommand
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public ConfirmEmailCommandHandler(UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<MessageResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı!"
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "E-posta doğrulama başarısız veya token geçersiz."
                };
            }

            await _mediator.Send(new CreateUserSettingsCommand
            {
                UserId = user.Id.ToString()
            });

            return new MessageResponse
            {
                Success = true,
                Message = "E-posta doğrulama başarılı! Kullanıcı ayarları oluşturuldu."
            };
        }
    }
}
