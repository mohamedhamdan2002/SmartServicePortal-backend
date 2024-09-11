namespace Application.Dtos.ReservationDtos;

public record ReservationDto
{
    public int Id { get; init; }
    public string Service { get; init; }
    public string Status { get; init; }
    public DateTime ReservationDate { get; init; }
}
