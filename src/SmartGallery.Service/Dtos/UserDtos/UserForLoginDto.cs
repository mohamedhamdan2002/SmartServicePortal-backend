namespace SmartGallery.Service.Dtos.UserDtos
{
    public record UserForLoginDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
