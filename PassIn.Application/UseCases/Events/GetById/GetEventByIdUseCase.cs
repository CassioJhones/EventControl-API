using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{

    public ResponseEventJson Execute(Guid id)
    {
        var bancoSQLite = new PassInDbContext();
        var evento = bancoSQLite.Events.Find(id);
        if (evento is null)
            throw new PassInException("Id nao existe");

        return new ResponseEventJson
        {
            Id = evento.Id,
            Title   = evento.Title,
            Details  = evento.Details,
            MaximumAttendees = evento.Maximum_Attendees,
            AttendeesAmount = -1
        };
    }
}
