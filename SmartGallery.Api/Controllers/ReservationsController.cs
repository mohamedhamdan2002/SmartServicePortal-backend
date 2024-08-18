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
            var customerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (customerEmail == null) return BadRequest();
            var result = await _reservationService.CreateReservationAsync(serviceId, customerEmail);
            return HandleResult<ReservationDto>(result);
        }
    }
}
