using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ReservationDtos;

namespace SmartGallery.Service.Implementation
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ReservationService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Result> CreateReservationAsync(int serviceId, string customerId)
        {
            var service = await _repositoryManager.ServiceRepository.GetByIdAsync(serviceId);
            if (service is null)
                return ApplicationErrors.BadRequestError;
            var reservation = new Reservation
            {
                CustomerId = customerId,
                ServiceId = serviceId
            };
            await _repositoryManager.ReservationRepository.CreateAsync(reservation);
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
    }
}
