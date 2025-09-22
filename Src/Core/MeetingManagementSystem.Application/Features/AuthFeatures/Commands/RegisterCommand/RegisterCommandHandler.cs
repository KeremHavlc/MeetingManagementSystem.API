using MediatR;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponseDto>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<MessageResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = request.Password
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(" ", result.Errors.Select(e => e.Description)));
            }
            return new MessageResponseDto
            {
                Success = true,
                Message = "Kayıt Başarılı!"
            };
        }
    }
}
