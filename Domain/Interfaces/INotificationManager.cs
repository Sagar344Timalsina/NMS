using Domain.Entities;

namespace Infrastructure.Services.Interface
{
    public interface INotificationManager
    {
        Task<bool> ProcessNotificationAsync(Notification notification);
    }
}
