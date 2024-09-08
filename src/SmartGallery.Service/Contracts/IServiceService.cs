using Microsoft.AspNetCore.Http;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Dtos.ServiceDtos;
using SmartGallery.Service.Specifications;

namespace SmartGallery.Service.Contracts
{
    public interface IServiceService
    {
        Task<Result> GetAllServicesAsync(SpecificationParameter specParams);
        Task<Result> GetServiceById(int Id);
        Task<Result> CreateServiceAsync(ServiceForCreateDto serviceForCreate);
        Task<Result> UpdateServiceAsync(int id, ServiceForUpdateDto serviceForUpdate);
        Task<Result> DeleteServiceAsync(int id);
    }
}
