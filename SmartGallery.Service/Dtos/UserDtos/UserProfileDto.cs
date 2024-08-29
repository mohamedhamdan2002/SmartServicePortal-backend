namespace SmartGallery.Service.Dtos.UserDtos
{
    public record UserProfileDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string Address { get; init; }
    }
}
