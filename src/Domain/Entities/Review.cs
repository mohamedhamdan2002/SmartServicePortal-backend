using Domain.Abstractions;

namespace Domain.Entities;

public class Review : Entity
{
    public string Title { get; set; }
    public string Comment { get; set; }
    public int Rate { get; set; }
    public bool AsAnonymous { get; set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public string CustomerId { get; set; }
    public User Customer { get; set; }
    public int ServiceId { get; set; }
}
