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
    public class ReservationSpecification : Specification<Reservation, ReservationDto>
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
}
