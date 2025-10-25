using MediatR;
using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Commands.CreateChatMessageCommand
{
    public class CreateChatMessageCommand : IRequest<ChatMessageDto>
    {
        public Guid MeetingId { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; }
    
    }
}
