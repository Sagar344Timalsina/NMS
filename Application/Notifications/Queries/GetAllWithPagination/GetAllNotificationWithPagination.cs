using Application.Common.DTOs.Notification;
using Application.Common.DTOs.User;
using Application.Users.Queries.GetUsersWithPagination;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Utils.Wrapper;

namespace Application.Notifications.Queries.GetAllWithPagination
{
    public record GetAllNotificationWithPagination(int userId, int pageNumber, string sortColumn, string sortDirection, string filters) : IRequest<IPagedResponse<IEnumerable<NotificationResponseDTO>>>
    {
        public int ListId { get; init; }
        public int UserId { get; init; } = userId;
        public int PageNumber { get; init; } = pageNumber;
        public int PageSize { get; init; } = 10;
        public string SortColumn { get; init; } = sortColumn;
        public string SortDirection { get; init; } = sortDirection;
        public string Filter { get; init; } = filters;
    }
    public class GetAllUsersWithPaginationQueryHandler(INotificationRepository notificationRepositories) : IRequestHandler<GetAllNotificationWithPagination, IPagedResponse<IEnumerable<NotificationResponseDTO>>>
    {
        public async Task<IPagedResponse<IEnumerable<NotificationResponseDTO>>> Handle(GetAllNotificationWithPagination request, CancellationToken cancellationToken)
        {
            var (notification, totalRecords) = await notificationRepositories.GetNotificationForUserAsync(
                request.UserId,
                 request.PageNumber,
                 request.PageSize,
                 request.SortColumn,
                 request.SortDirection,
            null
             );
            var notificationDtos = notification.Select(notify => new NotificationResponseDTO
            {
                Message = notify.Message,
                Id = notify.Id,
                Type=notify.Type,
                IsLive=notify.IsLive,
                IsRead=notify.IsRead,
                IsSent=notify.IsSent,
            });

            return new PagedResponse<IEnumerable<NotificationResponseDTO>>(
                notificationDtos,
                request.PageNumber,
                request.PageSize,
                totalRecords)
            {
                SortColumn = request.sortColumn,
                SortDirection = request.sortDirection,
                Filters = null
            };

        }
    }

}
