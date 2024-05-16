using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.MakeCheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    [HttpPost]
    [Route("{atendeId}")]
    [ProducesResponseType(typeof(ResponseRegisteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid atendeId)
    {
        var useCase = new MakeCheckinUseCase();
        var teste = useCase.Execute(atendeId);
        return Created();
    }
}
