using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ReservationDtos;
using System.Security.Claims;

namespace SmartGallery.Api.Controllers
{
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("{serviceId}")]
        public async Task<ActionResult<ReservationDto>> CreateReservation(int serviceId)
        {
            var customerId = "d18a4959-cf62-4955-b3d6-8a380a55a95d";
            var result = await _reservationService.CreateReservationAsync(serviceId, customerId);
            return HandleResult<ReservationDto>(result);
        }
    }
}
