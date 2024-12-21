

namespace Application.Common.DTOs.Notification
{
    public class NotificationRequestDTO
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsLive { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsSent { get; set; } = false;
    }
}
