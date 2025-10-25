using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;

namespace MeetingManagementSystem.Application.Common
{
    public interface IChatNotifier 
    {
        Task BroadcastMessageAsync(Guid meetingId, ChatMessageDto message, CancellationToken cancellationToken);
    }
}
