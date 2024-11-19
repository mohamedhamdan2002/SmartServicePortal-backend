using Application.Dtos.NotificationDtos;
using Application.Dtos.ReservationDtos;
using Application.Interfaces;
using Application.Services.Contracts;
using Application.Specifications;
using Domain.Entities;
using Domain.Enums;
using Domain.Errors;
using Domain.Repositories;

namespace Application.Services.Implementation;

public class ReservationService : IReservationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly INotificationService _notificationService;

    public ReservationService(IRepositoryManager repositoryManager, INotificationService notificationService)
    {
        _repositoryManager = repositoryManager;
        _notificationService = notificationService;
    }

    public async Task<Result<ReservationDto>> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationDto reservationForCreationDto)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(serviceId);
        if (service is null)
            return Result.Fail<ReservationDto>(ApplicationErrors.BadRequestError);
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
        return Result.Success(reservationDto);
    }

    public async Task<Result<IEnumerable<ReservationDetailsDto>>> GetAllReservationsAsync()
    {
        var spec = new ReservationWithDetailsSpecification();
        var resvations = await _repositoryManager.ReservationRepository.GetAllAsync(spec);
        return Result.Success(resvations);
    }

    public async Task<Result<IEnumerable<ReservationDto>>> GetReservationsForUserAsync(string customerId)
    {
        if (customerId is null)
            return Result.Fail<IEnumerable<ReservationDto>>(ApplicationErrors.BadRequestError);
        var spec = new ReservationSpecification(customerId);
        var reservations = await _repositoryManager.ReservationRepository.GetAllAsync(spec);
        return Result.Success(reservations);
    }

    public async Task<Result> RemoveReservationAsync(int id)
    {
        var reservation = await _repositoryManager.ReservationRepository.GetByIdAsync(id);
        if (reservation == null)
            return Result.Fail(ApplicationErrors.BadRequestError);
        _repositoryManager.ReservationRepository.Delete(reservation);
        await _repositoryManager.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> UpdateReservationStatusAsync(int id, StatusEnum status)
    {
        var spec = new ReservationWithServiceSpecification(id);
        var reservation = await _repositoryManager.ReservationRepository.GetBySpecAsync(spec);
        if (reservation == null)
            return Result.Fail(ApplicationErrors.BadRequestError);
        reservation.ChangeReservationStatus(status);
        await _repositoryManager.SaveChangesAsync();
        //await _notificationService.NotifyBy(reservation.CustomerId, reservation.Service.Name, reservation.Status);
        var notificationDto = new ReservationNotificationDto { Id = id, Service = reservation.Service.Name, Status = reservation.Status.ToString() };
        await _notificationService.NotifyBy(reservation.CustomerId, notificationDto);
        return Result.Success();
    }
}
