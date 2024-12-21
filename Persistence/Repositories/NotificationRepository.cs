using Application.Common.DTOs.Notification;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class NotificationRepository(AppDbContext context) : INotificationRepository
    {
        public async Task<bool> AddAsync(Notification notification)
        {
            try
            {
                context.Notifications.Add(notification);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Notification> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<Notification> Notifications, int TotalRecords)> GetNotificationForUserAsync(int userId, int pageNumber, int pageSize, string? sortColumn = null, string? sortDirection = null, IDictionary<string, string>? filters = null)
        {
            try
            {
                var query = context.Notifications.Where(x => x.User.Id == userId).AsQueryable();

                // Apply filters, sorting, and pagination logic (same as before)
                if (filters != null)
                {
                    // Filter logic
                }

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    query = sortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(e => EF.Property<object>(e, sortColumn))
                        : query.OrderBy(e => EF.Property<object>(e, sortColumn));
                }

                int totalRecords = await query.CountAsync();

                var notification = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (notification, totalRecords);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> updateAsync(Notification notification)
        {
            try
            {
                var existingNotification = await context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notification.Id);
                if (existingNotification == null)
                {
                    throw new KeyNotFoundException($"Notification with ID {notification.Id} not found.");
                }
                //context.Entry(existingNotification).CurrentValues.SetValues(notification);
                existingNotification.Message = notification.Message;
                existingNotification.Type = notification.Type;
                existingNotification.IsLive = notification.IsLive;
                existingNotification.IsSent = notification.IsSent;
                existingNotification.IsRead = notification.IsRead;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
