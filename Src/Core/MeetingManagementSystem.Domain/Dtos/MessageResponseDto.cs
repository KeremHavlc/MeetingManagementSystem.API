namespace MeetingManagementSystem.Domain.Dtos
{
    public sealed record MessageResponseDto
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
