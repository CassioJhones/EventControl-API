using PassIn.Communication.Requests;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    public void Execute(RequestEventJson request)
    {

        Validate(request);

    }

    public void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
            throw new PassInException("Numero maximo invalido");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new PassInException("Titulo invalido");

        if (string.IsNullOrWhiteSpace(request.Details))
            throw new PassInException("Detalhes invalidos");
    }
}
