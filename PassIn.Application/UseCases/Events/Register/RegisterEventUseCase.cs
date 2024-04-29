using PassIn.Communication.Requests;

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
            throw new ArgumentException("Numero maximo invalido");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Titulo invalido");

        if (string.IsNullOrWhiteSpace(request.Details))
            throw new ArgumentException("Detalhes invalidos");
    }
}
