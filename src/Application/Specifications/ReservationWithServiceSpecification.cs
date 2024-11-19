using Domain.Entities;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class ReservationWithServiceSpecification : Specification<Reservation>
    {
        public ReservationWithServiceSpecification(int id) : base(reservation => reservation.Id == id)
        {
            AddInclude(reservation => reservation.Service);
        }
    }
}
