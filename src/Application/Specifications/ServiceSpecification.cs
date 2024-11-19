﻿using Api.Utilities;
using Application.Dtos.ServiceDtos;
using Domain.Specifications;

namespace Application.Specifications;

public class ServiceSpecification : Specification<Domain.Entities.Service, ServiceDto>
{
    public ServiceSpecification(SpecificationParameters specParams)
        : base(
            service =>
                        (string.IsNullOrEmpty(specParams.Search) || service.Name.ToLower().Contains(specParams.Search!)) &&
                        (!(specParams.CategoriesIds != null) || specParams.CategoriesIds.Values.Any(value => value == service.CategoryId))
        )
    {
        AddInclude(service => service.Category);
        ApplySort(specParams.Sort);
        AddSelector();
        AddPagination(specParams.PageSize, specParams.PageIndex);
    }
    private void ApplySort(string? sort)
    {
        if (sort is null)
            OrderBy = service => service.Name;
        switch (sort)
        {
            case "cost":
                OrderBy = service => service.Cost;
                break;
            case "costDesc":
                OrderByDescending = service => service.Cost;
                break;
            default:
                OrderBy = service => service.Name;
                break;
        }
    }
    private void AddSelector()
    {
        Selector = service => new ServiceDto
        {
            Id = service.Id,
            Category = service.Category.Name,
            Name = service.Name,
            Cost = service.Cost,
            Description = service.Description,
            PictureUrl = $"{Constants.BaseUrl}{service.PictureUrl}",
            CategoryId = service.CategoryId,
        };
    }
    public ServiceSpecification(int id)
        : base(service => service.Id == id)
    {
        AddInclude(service => service.Category);
        AddSelector();
    }
}
