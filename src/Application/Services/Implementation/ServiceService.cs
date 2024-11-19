using Api.Utilities;
using Application.Dtos.ServiceDtos;
using Application.Services.Contracts;
using Application.Specifications;
using Application.Utilities;
using Domain.Errors;
using Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implementation;

public class ServiceService : IServiceService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IWebHostEnvironment _env;

    public ServiceService(IRepositoryManager repositoryManager, IWebHostEnvironment env)
    {
        _repositoryManager = repositoryManager;
        _env = env;
    }

    public async Task<Result<ServiceDto>> CreateServiceAsync(ServiceForCreateDto serviceForCreate)
    {
        var category = await _repositoryManager.CategoryRepository.GetByIdAsync(serviceForCreate.CategoryId);
        if (category is null)
            return Result.Fail<ServiceDto>(ApplicationErrors.BadRequestError);
        var pictureUrl = await HandleServicePictureAsync(serviceForCreate.Picture);
        if (pictureUrl == string.Empty)
            return Result.Fail<ServiceDto>(ApplicationErrors.BadRequestError);
        var service = serviceForCreate.ToEntity(pictureUrl: pictureUrl);
        _repositoryManager.ServiceRepository.Create(service);
        await _repositoryManager.SaveChangesAsync();
        var serviceDto = new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Category = category.Name,
            CategoryId = category.Id,
            PictureUrl = $"{Constants.BaseUrl}{service.PictureUrl}",
            Description = service.Description,
            Cost = service.Cost
        };
        return Result.Success(serviceDto);
    }

    public async Task<Result> DeleteServiceAsync(int id)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(id);
        if (service is null)
            return Result.Fail(ApplicationErrors.BadRequestError);
        DeleteServicePicture(service.PictureUrl);
        _repositoryManager.ServiceRepository.Delete(service);
        await _repositoryManager.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<Pagination<ServiceDto>>> GetAllServicesAsync(SpecificationParameters specParams)
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
            Count = 20,
        };
        return Result.Success(data);
    }

    public async Task<Result<ServiceDto>> GetServiceById(int id)
    {
        var spec = new ServiceSpecification(id);
        var service = await _repositoryManager.ServiceRepository.GetBySpecAsync(spec);
        if (service is null)
            return Result.Fail<ServiceDto>(ApplicationErrors.NotFoundError);
        return Result.Success(service);
    }

    public async Task<Result> UpdateServiceAsync(int id, ServiceForUpdateDto serviceForUpdate)
    {
        var service = await _repositoryManager.ServiceRepository.GetByIdAsync(id);
        if (service is null)
            return Result.Fail(ApplicationErrors.BadRequestError);
        if (service.CategoryId != serviceForUpdate.CategoryId)
        {
            var category = await _repositoryManager.CategoryRepository.GetByIdAsync(serviceForUpdate.CategoryId);
            if (category is null)
                return Result.Fail(ApplicationErrors.BadRequestError);
            service.CategoryId = category.Id;
        }
        DeleteServicePicture(service.PictureUrl);
        var pictureUrl = await HandleServicePictureAsync(serviceForUpdate.Picture);
        if (pictureUrl == string.Empty)
            return Result.Fail(ApplicationErrors.BadRequestError);
        serviceForUpdate.UpdateEntity(service, pictureUrl);
        await _repositoryManager.SaveChangesAsync();
        return Result.Success();
    }

    private async Task<string> HandleServicePictureAsync(IFormFile picture)
    {
        if (picture == null)
            return string.Empty;
        var uploadsFoldder = Path.Combine(_env.WebRootPath, "images");
        var uniqueFileName = $"{Guid.NewGuid().ToString()[..5]}_{picture.FileName}";
        var filePath = Path.Combine(uploadsFoldder, uniqueFileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await picture.CopyToAsync(fileStream);
        }
        return $"images/{uniqueFileName}";
    }
    private void DeleteServicePicture(string pictureUrl)
    {
        var filePath = Path.Combine(_env.WebRootPath, pictureUrl);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public async Task<Result> DeleteServicesAsync(CollectionOfIds servicesIds)
    {
        await _repositoryManager.ServiceRepository.DeleteRangeAsync(servicesIds.Values);
        return Result.Success();
    }
}
