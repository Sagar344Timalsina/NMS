using Application.Common.DTOs.Notification;
using Application.Users.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using ServiceLayer.Wrapper;

namespace Application.Notifications.Commands.Create
{
    public record AddNotificationCommand(NotificationRequestDTO Notification) : IRequest<Response>;
    public class AddNotificationCommandHandler(INotificationRepository repositories) : IRequestHandler<AddNotificationCommand, Response>
    {
        public async Task<Response> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            //return await repositories.addUser(request.user);
            Notification notification = new();
            notification.Type = request.Notification.Type;
            notification.Message = request.Notification.Message;
            notification.IsLive = request.Notification.IsLive;
            notification.IsSent = request.Notification.IsSent;
            notification.IsRead = request.Notification.IsRead;
            notification.UserId = request.Notification.UserId;
            var response = await repositories.AddAsync(notification);
            return new Response() { Succeeded = response, Messages = response == true ? "Successfully Added" : "Couldnot add Notification" };
        }
    }
}
