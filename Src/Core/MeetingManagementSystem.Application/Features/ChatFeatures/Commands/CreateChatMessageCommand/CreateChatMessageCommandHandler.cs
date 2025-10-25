using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Common;
using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;
using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Commands.CreateChatMessageCommand
{
    public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, ChatMessageDto>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatNotifier _chatNotifier;
        public CreateChatMessageCommandHandler(IChatMessageRepository chatMessageRepository, IChatNotifier chatNotifier)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatNotifier = chatNotifier;
        }

        public async Task<ChatMessageDto> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
        {
            var messageEntity = request.Adapt<ChatMessage>();
            messageEntity.CreatedAt = DateTime.UtcNow;

            await _chatMessageRepository.AddAsync(messageEntity, cancellationToken);
            var messageDto = messageEntity.Adapt<ChatMessageDto>();
            await _chatNotifier.BroadcastMessageAsync(messageDto.MeetingId, messageDto, cancellationToken);

            return messageDto;
        }
    }
}
