using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{

    public ResponseEventJson Execute(Guid id)
    {
        PassInDbContext bancoSQLite = new PassInDbContext();
        Event? evento = bancoSQLite.Events.Find(id);
        if (evento is null)
            throw new NotFoundException("Id nao existe");

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
