using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Api.Utilities;
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
        
        public ActionResult HandleResult<TResult>(Result result, ActionEnum action = ActionEnum.OkResult)
        {
            if(result is null)
                throw new ArgumentNullException(nameof(result));

            if (result.IsFailure)
                return HandleError(result.Error);

            return action switch
            {
                ActionEnum.OkResult => Ok(result.GetData<TResult>()),
                ActionEnum.NoContentResult => NoContent(),
                ActionEnum.CreatedAtResult => Ok(result.GetData<TResult>()), // will be change
                _ => throw new NotImplementedException()
            };
        }
    }
}
