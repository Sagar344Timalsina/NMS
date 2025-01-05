using Domain.Entities;

namespace Infrastructure.Services.Interface
{
    public interface INotificationManager
    {
        Task<bool> ProcessPublishNotificationAsync(Notification notification);
        Task<bool> ProcessConsumeNotificationAsync(Notification notification);
    }
}
