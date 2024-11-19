using Domain.Abstractions;

namespace Domain.Entities;

public class UsersConnection : Entity
{
    public required string UserId { get; set; }
    public required string ConnectionId { get; set; }
}
