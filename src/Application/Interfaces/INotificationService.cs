using Application.Dtos.NotificationDtos;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyBy(string userId, string service, StatusEnum status);
        Task NotifyBy(string userId, ReservationNotificationDto notificationDto);
    }
}
