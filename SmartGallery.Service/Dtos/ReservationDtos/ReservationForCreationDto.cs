namespace SmartGallery.Service.Dtos.ReservationDtos
{
    public record ReservationForCreationDto
    {
        public string ProblemDescription { get; init; }
        public AddressDto? AddressDto { get; init; }
        public ContactDto? ContactDto { get; init; }
    }
}
