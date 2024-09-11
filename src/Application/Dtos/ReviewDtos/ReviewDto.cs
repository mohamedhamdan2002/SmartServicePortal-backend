namespace Application.Dtos.ReviewDtos;

public record ReviewDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Comment { get; init; }
    public string CustomerName { get; init; }
    public DateTime CreatedAt { get; init; }
    public int Rate { get; init; }
    public bool AsAnonymous { get; init; }
}
