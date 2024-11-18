using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities;

public class Reservation : Entity
{
    public string CustomerId { get; set; }
    public User Customer { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; }
    public StatusEnum Status { get; private set; } = StatusEnum.Pending;
    public DateTime ReservationDate { get; private set; } = DateTime.Now;
    public string ProblemDescription { get; set; }
    public Address? Address { get; set; }
    public Contact? Contact { get; set; }

    public void ChangeReservationStatus(StatusEnum status)
    {
        Status = status;
    }
}
