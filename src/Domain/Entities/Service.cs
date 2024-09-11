namespace Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PictureUrl { get; set; }
    public decimal Cost { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
