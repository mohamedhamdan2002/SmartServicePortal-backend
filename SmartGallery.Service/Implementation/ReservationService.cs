using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ReservationDtos;
using SmartGallery.Service.Specifications;

namespace SmartGallery.Service.Implementation
{
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
            
            if(reservationForCreationDto.ContactDto is not null)
            {
                reservation.Contact = new Contact
                {
                    FirstName = reservationForCreationDto.ContactDto.FirstName,
                    LastName = reservationForCreationDto.ContactDto.LastName
                };
            }

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

        public async Task<Result> GetReservationsForUserAsync(string customerId)
        {
            if (customerId is null)
                return ApplicationErrors.BadRequestError;
            var spec = new ReservationSpecification(customerId);
            var reservations = await _repositoryManager.ReservationRepository.GetAllAsync(spec);
            return Result<IEnumerable<ReservationDto>>.Success(reservations);
        }
    }
}
