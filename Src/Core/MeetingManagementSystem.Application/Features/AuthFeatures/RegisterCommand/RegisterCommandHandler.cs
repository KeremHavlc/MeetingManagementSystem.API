using Mapster;
using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<AppUser>();
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
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
                Message = "Kullanıcı başarıyla kayıt edildi!"
            };
        }
    }
}
