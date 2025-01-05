

using Domain.Entities;
using Infrastructure.Services.Interface;
using MediatR;
using ServiceLayer.Wrapper;
using Shared.Utils.Wrapper;

namespace Application.Notifications.Commands.Create
{
    public record SendNotificationCommand(Notification notification) : IRequest<Response>;

    public class SendNotificationCommandHandler(INotificationManager notificationManager) : IRequestHandler<SendNotificationCommand, Response>
    {
        public async Task<Response> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await notificationManager.ProcessConsumeNotificationAsync(request.notification);
                return new Response() { Succeeded = res, Messages = res == true ? "Successfully Sent" : "Something went wrong!!!!" };
            }
            catch (Exception ex)
            {
                return (Response)await Response.FailAsync(ex.Message);
            }
        }
    }
}
