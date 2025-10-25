using Mapster;
using MediatR;
using MeetingManagementSystem.Application.Features.ChatFeatures.Dto;
using MeetingManagementSystem.Domain.Repositories;

namespace MeetingManagementSystem.Application.Features.ChatFeatures.Queries.GetMessagesByMeetingIdQuery
{
    public class GetMessagesByMeetingIdQueryHandler : IRequestHandler<GetMessagesByMeetingIdQuery, IReadOnlyList<ChatMessageDto>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public GetMessagesByMeetingIdQueryHandler(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<IReadOnlyList<ChatMessageDto>> Handle(GetMessagesByMeetingIdQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatMessageRepository.GetMeetingIdAsync(request.MeetingId, cancellationToken);

            return messages.Adapt<IReadOnlyList<ChatMessageDto>>();
        }
    }
}
