using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    public ResponseRegisteredJson Execute(RequestEventJson request)
    {
        Validate(request);

        PassInDbContext bancoSQLite = new();

        Event entidade = new()
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-"),
        };

        bancoSQLite.Events.Add(entidade);
        bancoSQLite.SaveChanges();
Log.LogToFile("Registro de Evento","Realizado com Sucesso");
        return new ResponseRegisteredJson
        {
            Id = entidade.Id
        };
    }

    public void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
            throw new ErrorOnValidationException("Numero maximo invalido");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ErrorOnValidationException("Titulo invalido");

        if (string.IsNullOrWhiteSpace(request.Details))
            throw new ErrorOnValidationException("Detalhes invalidos");
    }
}
