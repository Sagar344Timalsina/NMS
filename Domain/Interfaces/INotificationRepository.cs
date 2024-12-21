using Domain.Entities;
using System.Text.Json.Serialization.Metadata;

namespace Domain.Interfaces;

public interface INotificationRepository
{
    Task<Notification> GetByIdAsync(int Id);
    Task<(IEnumerable<Notification> Notifications, int TotalRecords)> GetNotificationForUserAsync(
        int userId,
        int pageNumber,
        int pageSize,
        string? sortColumn = null,
        string? sortDirection = null,
        IDictionary<string, string>? filters = null);
    Task<bool> AddAsync(Notification notification);
    Task<bool> updateAsync(Notification notification);
}