using MeetingManagementSystem.Application.Common;
using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;
using Microsoft.AspNetCore.SignalR;

namespace MeetingManagementSystem.Infrastructure.RealTime
{
    public class ChatNotifier : IChatNotifier
    {
        private readonly IHubContext<Hub> _hubContext;

        public ChatNotifier(IHubContext<Hub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastMessageAsync(Guid meetingId, ChatMessageDto message, CancellationToken cancellationToken)
        {
            await _hubContext.Clients
                .Group(meetingId.ToString())
                .SendAsync("ReceiveMessage", message, cancellationToken);
        }
    }
}
