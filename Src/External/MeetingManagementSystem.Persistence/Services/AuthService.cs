using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.LoginCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.RegisterCommand;
using MeetingManagementSystem.Application.Features.AuthFeatures.Commands.SignInCommand;
using MeetingManagementSystem.Application.Services;
using MeetingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagementSystem.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.UserNameOrEmail || e.UserName == request.UserNameOrEmail);
            if(user == null)
            {
                return false;
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            return result;            
        }

        public async Task<IdentityResult> RegisterAsync(RegisterCommand request, CancellationToken cancellationToken)
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
            return result;
        }

        public async Task<(SignInResult? Result , AppUser? User)> SignIn(SignInCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(e => e.Email == request.UserNameOrEmail || e.UserName == request.UserNameOrEmail);
            if(user == null)
            {
                return (null , null);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            return (result , user);
        }
    }
}
