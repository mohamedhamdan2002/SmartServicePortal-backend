using Application.Dtos.ReservationDtos;
using Domain.Enums;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IReservationService
{
    Task<Result<ReservationDto>> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationDto reservationForCreationDto);
    Task<Result<IEnumerable<ReservationDto>>> GetReservationsForUserAsync(string customerId);
    Task<Result<IEnumerable<ReservationDetailsDto>>> GetAllReservationsAsync();
    Task<Result> RemoveReservationAsync(int id);
    Task<Result> UpdateReservationStatusAsync(int id, StatusEnum status);
}
