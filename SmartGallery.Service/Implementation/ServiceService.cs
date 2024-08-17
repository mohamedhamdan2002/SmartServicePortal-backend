using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ServiceDtos;
using SmartGallery.Service.Specifications;
using SmartGallery.Service.Utilities;

namespace SmartGallery.Service.Implementation
{
    public class ServiceService : IServiceService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ServiceService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<Result> GetAllServicesAsync(SpecificationParameter specParams)
        {
            var spec = new ServiceSpecification(specParams);
            var services = await _repositoryManager.ServiceRepository.GetAllAsync(spec);
            var countSpec = new ServiceCountSpecification(specParams);
            var count = await _repositoryManager.ServiceRepository.CountAsync(countSpec);
            var data = new Pagination<ServiceDto>
            {
                Data = services.ToList(),
                PageSize = specParams.PageSize,
                PageIndex = specParams.PageIndex,
                Count = count,
            };
            return Result<Pagination<ServiceDto>>.Success(data);
        }

        public async Task<Result> GetServiceById(int id)
        {
            var spec = new ServiceSpecification(id);
            var service = await _repositoryManager.ServiceRepository.GetBySpecAsync(spec);
            if (service is null)
                return ApplicationErrors.NotFoundError;
            return Result<ServiceDto>.Success(service);
        }
    }
}
