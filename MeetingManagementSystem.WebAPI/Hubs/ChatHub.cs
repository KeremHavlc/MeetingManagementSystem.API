using MediatR;
using MeetingManagementSystem.Application.Features.ChatFeatures.Commands.CreateChatMessageCommand;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MeetingManagementSystem.WebAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task OnConnectedAsync()
        {
            var hasUser = Context.User != null;
            var claimCount = Context.User?.Claims?.Count() ?? 0;

            Console.WriteLine($"SignalR Connected → User Exists: {hasUser}, Claim Count: {claimCount}");

            await base.OnConnectedAsync();
        }

        public async Task JoinMeetingGroup(string meetingId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, meetingId);
        }

        public async Task SendMessage(string meetingId, string senderId, string message)
        {
            if (!Guid.TryParse(meetingId, out Guid meetingGuid))
                throw new HubException("Geçersiz meetingId!");

            if (!Guid.TryParse(senderId, out Guid senderGuid))
                throw new HubException("Geçersiz senderId!");

            var command = new CreateChatMessageCommand
            {
                MeetingId = meetingGuid,
                SenderId = senderGuid,
                Message = message
            };

            var result = await _mediator.Send(command);

            await Clients.Group(meetingId).SendAsync("ReceiveMessage", result);
        }

    }
}
