using Application.Dtos.NotificationDtos;
using Application.Dtos.ReservationDtos;

namespace Infrastructure.Hubs.Notification;

public interface INotificationClient
{
    Task onReservationUpdated(NotificationDto notification);
    Task onReservationUpdated(ReservationNotificationDto notification);

}
