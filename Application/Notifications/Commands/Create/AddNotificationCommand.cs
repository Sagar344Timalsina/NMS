using Application.Common.DTOs.Notification;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Services.Interface;
using MediatR;
using ServiceLayer.Wrapper;

namespace Application.Notifications.Commands.Create
{
    public record AddNotificationCommand(NotificationRequestDTO Notification) : IRequest<Response>;

    public class AddNotificationCommandHandler( INotificationManager notificationManager) : IRequestHandler<AddNotificationCommand, Response>
    {
        public async Task<Response> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //return await repositories.addUser(request.user);
                Notification notification = new();
                notification.Type = request.Notification.Type;
                notification.Message = request.Notification.Message;
                notification.IsLive = request.Notification.IsLive;
                notification.IsSent = request.Notification.IsSent;
                notification.IsRead = request.Notification.IsRead;
                notification.UserId = request.Notification.UserId;

                var response = await notificationManager.ProcessNotificationAsync(notification);
                //var response = await repositories.AddAsync(notification);
                return new Response() { Succeeded = response, Messages = response == true ? "Successfully Added" : "Couldnot add Notification" };
            }
            catch (Exception ex)
            {
                return (Response)await Response.FailAsync(ex.Message);
            }
        }
    }
}
