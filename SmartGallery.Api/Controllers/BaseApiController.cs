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
                StatusCodes.Status401Unauthorized => Unauthorized(error),
                _ => throw new NotImplementedException()
            }; ;
        }
        public ActionResult HandleResult<TResult>(Result result)
        {
            if (result.IsFailure)
                return HandleError(result.Error);

            return Ok(result.GetData<TResult>());
        }
    }
}
