using MediatR;
using MeetingManagementSystem.Application.Services;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);
            if (!result.Succeeded)
            {
                return new MessageResponse
                {
                    Success = false,
                    Message = string.Join(" ", result.Errors.Select(e => e.Description))
                };
            }
            return new MessageResponse
            {
                Success = true,
                Message = "Kullanıcı Kaydı Başarılı!"
            };
        }
    }
}
