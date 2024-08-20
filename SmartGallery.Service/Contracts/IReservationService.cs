using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Dtos.ReservationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Service.Contracts
{
    public interface IReservationService
    {
        Task<Result> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationDto reservationForCreationDto);
    }
}
