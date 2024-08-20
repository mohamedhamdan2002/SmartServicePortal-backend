namespace SmartGallery.Service.Dtos.ReservationDtos
{
    public record AddressDto
    {
        public string? Street { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }
    }
}
