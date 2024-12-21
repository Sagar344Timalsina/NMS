using Application.Common.DTOs.Notification;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using ServiceLayer.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Commands.Update
{
    public record UpdateNotificationCommand(NotificationUpdateRequestDTO requestModel):IRequest<Response>;

    public class UpdateNotificationCommandHandler(INotificationRepository _notification) : IRequestHandler<UpdateNotificationCommand, Response>
    {
        public async Task<Response> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Notification notification = new();
                notification.Message = request.requestModel.Message;
                notification.Type = request.requestModel.Type;
                notification.Id = request.requestModel.Id;
                notification.UserId = request.requestModel.UserId;
                notification.IsLive = request.requestModel.IsLive;
                notification.IsSent = request.requestModel.IsSent;
                notification.IsRead= request.requestModel.IsRead;
                var response = await _notification.updateAsync(notification);
                return new Response(){ Succeeded=response,Messages="Successfully Updated!!!"};
            }
            catch (Exception ex)
            {
                return (Response)await Response.FailAsync(ex.Message);
            }
        }
    }
}
