using Api.Utilities;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ActionResult HandleError(Error error)
    {
        if (error is null || error.StatusCode == StatusCodes.Status200OK)
            throw new ArgumentNullException(nameof(error));

        return error.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(error),
            StatusCodes.Status400BadRequest => BadRequest(error),
            StatusCodes.Status401Unauthorized => Unauthorized(error),
            _ => throw new NotImplementedException()
        };
    }

    protected ActionResult HandleResult(Result result)
    {
        ValdateResult(result);
        if (result.IsFailure)
            return HandleError(result.Error);
        return NoContent();
    }

    protected ActionResult HandleResult<TResult>(Result<TResult> result, ActionEnum action = ActionEnum.OkResult)
    {
        ValdateResult(result);

        if (result.IsFailure)
            return HandleError(result.Error);

        return action switch
        {
            ActionEnum.OkResult => Ok(result.Data),
            ActionEnum.CreatedAtResult => StatusCode(StatusCodes.Status201Created, result.Data),
            _ => throw new NotImplementedException()
        };
    }

    private void ValdateResult(Result result)
    {
        if (result is null)
            throw new ArgumentNullException(nameof(result));
    }

}
