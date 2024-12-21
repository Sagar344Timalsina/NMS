namespace Application.Common.DTOs.Notification
{
    public class NotificationResponseDTO
    {
        public int Id { get; set; }
        public string Message{ get; set; }
        public string Type {  get; set; }
        public bool IsRead {  get; set; }
        public bool IsSent{  get; set; }
        public bool IsLive{  get; set; }

    }
}
