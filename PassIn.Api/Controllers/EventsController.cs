using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEventJson) , StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson) , StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        try
        {
            RegisterEventUseCase useCase = new RegisterEventUseCase();
            useCase.Execute(request);

            return Created();
        }
        catch (ArgumentException erro)
        {
            return BadRequest(new ResponseErrorJson(erro.Message));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Erro Desconhecido"));
        }
    }
}
