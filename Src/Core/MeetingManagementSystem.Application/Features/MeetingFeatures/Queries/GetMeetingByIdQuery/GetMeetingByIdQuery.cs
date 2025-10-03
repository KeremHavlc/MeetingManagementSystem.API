using MediatR;
using MeetingManagementSystem.Domain.Dtos;

namespace MeetingManagementSystem.Application.Features.MeetingFeatures.Queries.GetMeetingByIdQuery
{
    public class GetMeetingByIdQuery : IRequest<MessageResponse>
    {
        public string MeetingId { get; set; }
    }
}
