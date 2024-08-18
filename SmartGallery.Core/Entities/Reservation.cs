namespace SmartGallery.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public string CustomerId { get; set; }
        public User Customer { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public StatusEnum Status { get; set; } = StatusEnum.Pending;
        public DateTime ReservationDate { get; set; } = DateTime.UtcNow;

    }

}
