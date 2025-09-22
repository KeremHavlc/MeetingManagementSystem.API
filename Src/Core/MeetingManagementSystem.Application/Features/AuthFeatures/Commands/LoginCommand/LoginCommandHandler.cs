using MediatR;
using MeetingManagementSystem.Application.Services;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MessageResponse>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<MessageResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            bool loginResult = await _authService.LoginAsync(request, cancellationToken);
            if (loginResult)
            {
                return new MessageResponse
                {
                    Success = true,
                    Message = "Giriş Başarılı!"
                };
            }
            return new MessageResponse
            {
                Success = false,
                Message = "Kullanıcı adı veya Şifre Hatalıdır!"
            };
               
        }
    }
}
