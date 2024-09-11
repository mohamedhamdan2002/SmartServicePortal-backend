using Application.Dtos.ReservationDtos;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IReservationService
{
    Task<Result> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationDto reservationForCreationDto);
    Task<Result> GetReservationsForUserAsync(string customerId);
    Task<Result> GetAllReservations();
}
