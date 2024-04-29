using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
		try
		{
            RegisterEventUseCase useCase = new RegisterEventUseCase();
			useCase.Execute(request);

			return Ok();
		}
		catch (ArgumentException erro)
		{
            return BadRequest($"{erro.Message}");
        }
    }
}
