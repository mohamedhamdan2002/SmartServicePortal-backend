using SmartGallery.Core.Entities;
using SmartGallery.Core.Specifications;
using SmartGallery.Service.Dtos.ReservationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Specifications
{
    public class ReservationWithDetailsSpecification : Specification<Reservation, ReservationDetailsDto>
    {
        public ReservationWithDetailsSpecification()
        {
            AddInclude(reservation => reservation.Customer);
            AddInclude(reservation => reservation.Service);
            Selector = reservation => new ReservationDetailsDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ProblemDescription = reservation.ProblemDescription,
                CustomerEmail = reservation.Customer.Email!,
                CustomerAddress = reservation.Customer.Address,
                CustomerName = $"{reservation.Customer.FirstName}{reservation.Customer.LastName}",
                Status = reservation.Status.ToString(),
                Service = reservation.Service.Name,
                Address = reservation.Address != null ? new AddressDto
                {
                    City = reservation.Address.City,
                    Country = reservation.Address.Country,
                    Street = reservation.Address.Street
                } : default,
                Contact = reservation.Contact != null ? new ContactDto
                {
                    FirstName = reservation.Contact.FirstName,
                    LastName = reservation.Contact.LastName,
                    PhoneNumber = reservation.Contact.PhoneNumber
                } : default,
            };
        }
    }
}
