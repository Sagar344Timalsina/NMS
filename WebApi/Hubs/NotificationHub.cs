using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    [AllowAnonymous]
    public class NotificationHub : Hub
    {
        public async Task BroadcastMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }
        public override async Task OnConnectedAsync()
        {
            // Optional: Log the connected user's ID
            //var userId = Context.UserIdentifier;
            //await base.OnConnectedAsync();
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined!!!!");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Optional: Handle user disconnection
            await base.OnDisconnectedAsync(exception);
        }
    }
}
