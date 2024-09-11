using Application.Dtos.ReservationDtos;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers;

//[Authorize]
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
    [HttpGet("customer")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsForCurrentUser()
    {
        var customerId = User.FindFirstValue("uid");
        var result = await _reservationService.GetReservationsForUserAsync(customerId!);
        return HandleResult<IEnumerable<ReservationDto>>(result);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDetailsDto>>> GetAllReservations()
    {
        var result = await _reservationService.GetAllReservations();
        return HandleResult<IEnumerable<ReservationDetailsDto>>(result);
    }


}
