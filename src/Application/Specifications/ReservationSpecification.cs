using Application.Dtos.ReservationDtos;
using Domain.Entities;
using Domain.Specifications;


namespace Application.Specifications;

public class ReservationSpecification : SpecificationWithResultType<Reservation, ReservationDto>
{
    public ReservationSpecification(string customerId)
        : base(reservation => reservation.CustomerId == customerId)
    {
        AddInclude(reservation => reservation.Service);
        Selector = reservation => new ReservationDto
        {
            Id = reservation.Id,
            ReservationDate = reservation.ReservationDate,
            Service = reservation.Service.Name,
            Status = reservation.Status.ToString()
        };
    }
}
