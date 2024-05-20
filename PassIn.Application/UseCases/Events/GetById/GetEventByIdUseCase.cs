using Microsoft.EntityFrameworkCore;
using PassIn.Application.LogFiles;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid id)
    {
        PassInDbContext bancoSQLite = new();
        Event? evento = bancoSQLite.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == id)
            ?? throw new NotFoundException("Id nao existe");

        Log.LogToFile("Busca por Evento", "Sucesso");
        return new ResponseEventJson
        {
            Id = evento.Id,
            Title = evento.Title,
            Details = evento.Details,
            MaximumAttendees = evento.Maximum_Attendees,
            AttendeesAmount = evento.Attendees.Count,
        };
    }
}
