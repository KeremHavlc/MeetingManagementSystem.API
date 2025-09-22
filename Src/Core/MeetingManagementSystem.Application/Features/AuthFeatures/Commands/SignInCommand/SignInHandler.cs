using MediatR;
using MeetingManagementSystem.Application.Services;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.SignInCommand
{
    public class SignInHandler : IRequestHandler<SignInCommand, MessageResponse>
    {
        private readonly IAuthService _authService;

        public SignInHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<MessageResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var (result,user) = await _authService.SignIn(request, cancellationToken);
            if(result == null || user == null)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Kullanıcı Bulunamadı!"
                };
            }
            if (result.IsLockedOut)
            {
                TimeSpan? timeSpan = user.LockoutEnd - DateTime.Now;
                if(timeSpan is not null)
                {
                    return new MessageResponse
                    {
                        Success = false,
                        Message = $"Şifrenizi 3 kere yanlış girdiniz. : {timeSpan.Value.TotalSeconds} Kilitlenmiştir."
                    };
                }
                else
                {
                    return new MessageResponse
                    {
                        Success = true,
                        Message = "Şifrenizi 3 kere yanlış girdiniz:30 saniye sonra tekrar deneyiniz."
                    };
                }
            }
            if (result.IsNotAllowed)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Mail adresiniz onaylı değildir."
                };
            }
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = "Şifreniz Hatalıdır!"
                };
            }
            return new MessageResponse
            {
                Success = true,
                Message = "Giriş Başarılıdır!"
            };
        }
    }
}
