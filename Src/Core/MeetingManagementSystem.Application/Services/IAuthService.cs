using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.SignInCommand;
using MeetingManagementSystem.Domain.Dtos;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MeetingManagementSystem.Application.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterCommand request, CancellationToken cancellationToken);
        Task<bool> LoginAsync(LoginCommand request, CancellationToken cancellationToken);
        Task<(SignInResult? Result , AppUser? User)> SignIn(SignInCommand request, CancellationToken cancellationToken);
    }
}
