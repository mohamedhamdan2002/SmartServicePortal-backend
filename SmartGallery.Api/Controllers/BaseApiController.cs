using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Core.Errors;

namespace SmartGallery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        public ActionResult HandleError(Error error)
        {
            return error.StatusCode switch
            {
                StatusCodes.Status404NotFound => NotFound(error),
                StatusCodes.Status400BadRequest => BadRequest(error),
                _ => throw new NotImplementedException()
            }; ;
        } 
    }
}
