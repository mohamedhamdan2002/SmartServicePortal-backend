using SmartGallery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Dtos.ReservationDtos
{
    public record ReservationDto
    {
        public int Id { get; init; }
        public string Service { get; init; }
        public string Status { get; init; } 
        public DateTime ReservationDate { get; init; }
    }
}
