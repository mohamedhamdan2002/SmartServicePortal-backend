using SmartGallery.Core.Entities;
using SmartGallery.Core.Errors;
using SmartGallery.Core.Repositories;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ServiceDtos;
using SmartGallery.Service.Specifications;
using SmartGallery.Service.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartGallery.Service.Implementation
{
    public class ServiceService : IServiceService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ServiceService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Result> CreateServiceAsync(ServiceForCreateDto serviceForCreate)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(serviceForCreate.CategoryId);
            if (category is null)
                return ApplicationErrors.BadRequestError;
            var service = new Core.Entities.Service()
            {
                CategoryId = category.Id,
                Cost = serviceForCreate.Cost,
                Description = serviceForCreate.Description,
                PictureUrl = serviceForCreate.PictureUrl,
                Name = serviceForCreate.Name
            };
            await _repositoryManager.ServiceRepository.CreateAsync(service);
            await _repositoryManager.SaveChangesAsync();
            var serviceDto = new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Category = category.Name,
                PictureUrl = service.PictureUrl,
                Description = service.Description,
                Cost = service.Cost
            };
            return Result<ServiceDto>.Success(serviceDto);

        }

        public async Task<Result> DeleteServiceAsync(int id)
        {
            var service = await _repositoryManager.ServiceRepository.GetByIdAsync(id);
            if (service is null)
                return ApplicationErrors.BadRequestError;
            _repositoryManager.ServiceRepository.Delete(service);
            await _repositoryManager.SaveChangesAsync();
            return Result.Success();   
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

        public async Task<Result> UpdateServiceAsync(int id, ServiceForUpdateDto serviceForUpdate)
        {
            var service = await _repositoryManager.ServiceRepository.GetByIdAsync(id);
            if (service is null)
                return ApplicationErrors.BadRequestError;
            if(service.CategoryId != serviceForUpdate.CategoryId)
            {
                var category = await _repositoryManager.CategoryRepository.GetByIdAsync(serviceForUpdate.CategoryId);
                if (category is null)
                    return ApplicationErrors.BadRequestError;
                service.CategoryId = category.Id;
            }
            service.Name = serviceForUpdate.Name;
            service.Cost = serviceForUpdate.Cost;
            service.PictureUrl = serviceForUpdate.PictureUrl;
            service.Description = serviceForUpdate.Description;
            await _repositoryManager.SaveChangesAsync();
            return Result.Success();
        }
    }
}
