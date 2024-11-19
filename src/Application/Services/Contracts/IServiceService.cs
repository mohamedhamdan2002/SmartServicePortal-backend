using Application.Dtos.ServiceDtos;
using Application.Specifications;
using Application.Utilities;
using Domain.Errors;

namespace Application.Services.Contracts;

public interface IServiceService
{
    Task<Result<Pagination<ServiceDto>>> GetAllServicesAsync(SpecificationParameters specParams);
    Task<Result<ServiceDto>> GetServiceById(int Id);
    Task<Result<ServiceDto>> CreateServiceAsync(ServiceForCreateDto serviceForCreate);
    Task<Result> UpdateServiceAsync(int id, ServiceForUpdateDto serviceForUpdate);
    Task<Result> DeleteServiceAsync(int id);
    Task<Result> DeleteServicesAsync(CollectionOfIds servicesIds);
}
