using Microsoft.AspNetCore.Mvc;
using SmartGallery.Api.Utilities;
using SmartGallery.Core.Errors;
using SmartGallery.Service.Contracts;
using SmartGallery.Service.Dtos.ServiceDtos;
using SmartGallery.Service.Specifications;
using SmartGallery.Service.Utilities;

namespace SmartGallery.Api.Controllers
{
    public class ServicesController : BaseApiController
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ServiceDto>>> GetAllServices([FromQuery] SpecificationParameter specParams)
        {
            var result = await _service.GetAllServicesAsync(specParams);
            return HandleResult<Pagination<ServiceDto>>(result);
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetServiceById(int id)
        {
            var result = await _service.GetServiceById(id);
            return HandleResult<ServiceDto>(result);
        }
        [HttpPost]

        public async Task<ActionResult<ServiceDto>> CreateService([FromForm] ServiceForCreateDto service)
        {
            var result = await _service.CreateServiceAsync(service);
            return HandleResult<ServiceDto>(result, ActionEnum.CreatedAtResult);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromForm] ServiceForUpdateDto service)
        {
            var result = await _service.UpdateServiceAsync(id, service);
            return HandleResult<Result>(result, ActionEnum.NoContentResult);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _service.DeleteServiceAsync(id);
            return HandleResult<Result>(result, ActionEnum.NoContentResult);
        }

    }
}
