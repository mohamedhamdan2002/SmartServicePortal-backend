using Api.Utilities;
using Application.Dtos.ServiceDtos;
using Application.Services.Contracts;
using Application.Specifications;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ServicesController : BaseApiController
{
    private readonly IServiceService _service;

    public ServicesController(IServiceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ServiceDto>>> GetAllServices([FromQuery] SpecificationParameters specParams)
    {
        var result = await _service.GetAllServicesAsync(specParams);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetServiceById(int id)
    {
        var result = await _service.GetServiceById(id);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> CreateService([FromForm] ServiceForCreateDto service)
    {
        var result = await _service.CreateServiceAsync(service);
        return HandleResult(result, ActionEnum.CreatedAtResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id, [FromForm] ServiceForUpdateDto service)
    {
        var result = await _service.UpdateServiceAsync(id, service);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        var result = await _service.DeleteServiceAsync(id);
        return HandleResult(result);
    }

    [HttpDelete("collection")]
    public async Task<IActionResult> DeleteCollectionOfCategories(CollectionOfIds servicesIds)
    {
        var result = await _service.DeleteServicesAsync(servicesIds);
        return HandleResult(result);
    }
}
