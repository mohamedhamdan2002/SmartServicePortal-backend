using SmartGallery.Core.Errors;
using SmartGallery.Service.Specifications;

namespace SmartGallery.Service.Contracts
{
    public interface IServiceService
    {
        Task<Result> GetAllServicesAsync(SpecificationParameter specParams);
        Task<Result> GetServiceById(int Id);
    }
}
