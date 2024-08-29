using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ReservationDtos;
using System.Security.Claims;

namespace SmartGallery.Api.Controllers
{
    [Authorize]
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }   

        [HttpPost("{serviceId}")]
        public async Task<ActionResult<ReservationDto>> CreateReservation(int serviceId, ReservationForCreationDto reservation)
        {
            var customerId = User.FindFirstValue("uid");
            var result = await _reservationService.CreateReservationAsync(serviceId, customerId, reservation);
            return HandleResult<ReservationDto>(result);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsForCurrentUser()
        {
            var customerId = User.FindFirstValue("uid");
            var result = await _reservationService.GetReservationsForUserAsync(customerId!);
            return HandleResult<IEnumerable<ReservationDto>>(result);
        }
    }
}
