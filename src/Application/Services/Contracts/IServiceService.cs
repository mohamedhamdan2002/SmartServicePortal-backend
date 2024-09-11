using Application.Dtos.ServiceDtos;
using Application.Specifications;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IServiceService
{
    Task<Result> GetAllServicesAsync(SpecificationParameter specParams);
    Task<Result> GetServiceById(int Id);
    Task<Result> CreateServiceAsync(ServiceForCreateDto serviceForCreate);
    Task<Result> UpdateServiceAsync(int id, ServiceForUpdateDto serviceForUpdate);
    Task<Result> DeleteServiceAsync(int id);
}
