using Application.Dtos.ReservationDtos;
using Application.Services.Contracts;
using Application.Specifications;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;

namespace Application.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly IRepositoryManager _repositoryManager;

    public ReservationService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationDto reservationForCreationDto)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(serviceId);
        if (service is null)
            return ApplicationErrors.BadRequestError;
        var reservation = new Reservation
        {
            CustomerId = customerId,
            ServiceId = serviceId,
            ProblemDescription = reservationForCreationDto.ProblemDescription
        };
        if (reservationForCreationDto.AddressDto is not null)
        {
            reservation.Address = new Address
            {
                City = reservationForCreationDto.AddressDto.City,
                Street = reservationForCreationDto.AddressDto.Street,
                Country = reservationForCreationDto.AddressDto.Country
            };
        }

        if (reservationForCreationDto.ContactDto is not null)
        {
            reservation.Contact = new Contact
            {
                FirstName = reservationForCreationDto.ContactDto.FirstName,
                LastName = reservationForCreationDto.ContactDto.LastName
            };
        }

        _repositoryManager.ReservationRepository.Create(reservation);
        await _repositoryManager.SaveChangesAsync();
        var reservationDto = new ReservationDto
        {
            Id = reservation.Id,
            ReservationDate = reservation.ReservationDate,
            Service = service.Name,
            Status = reservation.Status.ToString()
        };
        return Result<ReservationDto>.Success(reservationDto);
    }

    public async Task<Result> GetAllReservations()
    {
        var spec = new ReservationWithDetailsSpecification();
        var resvations = await _repositoryManager.ReservationRepository.GetAllAsync(spec);
        return Result<IEnumerable<ReservationDetailsDto>>.Success(resvations);
    }

    public async Task<Result> GetReservationsForUserAsync(string customerId)
    {
        if (customerId is null)
            return ApplicationErrors.BadRequestError;
        var spec = new ReservationSpecification(customerId);
        var reservations = await _repositoryManager.ReservationRepository.GetAllAsync(spec);
        return Result<IEnumerable<ReservationDto>>.Success(reservations);
    }
}
