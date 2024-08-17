using Microsoft.AspNetCore.Mvc;
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
            var data = result.GetData<Pagination<ServiceDto>>();
            return Ok(data);
        }


    }
}
