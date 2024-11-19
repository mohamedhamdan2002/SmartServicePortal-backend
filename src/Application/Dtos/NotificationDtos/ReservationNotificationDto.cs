namespace Application.Dtos.NotificationDtos;

public record ReservationNotificationDto
{
    public int Id { get; init; }
    public string Service { get; init; }
    public string Status { get; init; }
}
