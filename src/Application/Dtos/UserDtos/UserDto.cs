namespace Application.Dtos.UserDtos;

public record UserDto
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Token { get; init; }
}
