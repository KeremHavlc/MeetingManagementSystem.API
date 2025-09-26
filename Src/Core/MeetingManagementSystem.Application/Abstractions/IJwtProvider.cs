using MeetingManagementSystem.Domain.Entities;

namespace MeetingManagementSystem.Application.Abstractions
{
    public interface IJwtProvider
    {
        string CreateToken(AppUser appUser);
    }
}
