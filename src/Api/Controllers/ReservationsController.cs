using Application.Dtos.ReservationDtos;
using Application.Services.Contracts;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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
        var customerId = User.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
        var result = await _reservationService.CreateReservationAsync(serviceId, customerId, reservation);
        return HandleResult(result);
    }

    [HttpGet("customer")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsForCurrentUser()
    {
        var customerId = User.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
        var result = await _reservationService.GetReservationsForUserAsync(customerId!);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDetailsDto>>> GetAllReservations()
    {
        var result = await _reservationService.GetAllReservationsAsync();
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveReservation(int id)
    {
        var result = await _reservationService.RemoveReservationAsync(id);
        return HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservationStatus(int id, string status)
    {
        if (!Enum.TryParse(status, true, out StatusEnum reservationStatus))
        {
            return BadRequest("Invalid status input");
        }
        var result = await _reservationService.UpdateReservationStatusAsync(id, reservationStatus);
        return HandleResult(result);
    }

    [HttpGet("status")]
    public IActionResult GetReservationStatus()
    {
        var statusValues = Enum.GetValues<StatusEnum>().Select(s => new
        {
            Value = (int)s,
            status = s.ToString()
        });
        return Ok(statusValues);
    }
}
