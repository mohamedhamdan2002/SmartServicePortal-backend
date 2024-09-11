namespace Application.Dtos.ReservationDtos;

public record ReservationDetailsDto : ReservationDto
{
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerAddress { get; init; }
    public string ProblemDescription { get; init; }
    public AddressDto? Address { get; init; }

    public ContactDto? Contact { get; init; }
}
