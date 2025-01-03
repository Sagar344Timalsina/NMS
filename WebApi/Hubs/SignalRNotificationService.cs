using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class SignalRNotificationService:INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", notification.Message);
            //await _hubContext.Clients.User(notification.UserId.ToString()).SendAsync("ReceiveMessage", notification.Message);
            //await _hubContext.Clients.All.
        }
    }
}
