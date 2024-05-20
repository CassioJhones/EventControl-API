using Microsoft.EntityFrameworkCore;
using PassIn.Application.LogFiles;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _bancoSQL;
    public GetAllAttendeesByEventIdUseCase()
        => _bancoSQL = new PassInDbContext();

    public ResponseAllAttendeesJson Execute(Guid eventId)
    {
        Event? convidado = _bancoSQL.Events
            .Include(evento => evento.Attendees)
            .ThenInclude(convidado => convidado.CheckIn)
            .FirstOrDefault(evento => evento.Id == eventId);

        Log.LogToFile("Listagem de Participantes", "Realizado com Sucesso");
        return convidado is null
            ? throw new NotFoundException("Nao Existe")
            : new ResponseAllAttendeesJson
            {
                Attendees = convidado.Attendees.Select(convidado => new ResponseAttendeeJson
                {
                    Id = convidado.Id,
                    Name = convidado.Name,
                    Email = convidado.Email,
                    CreatedAt = convidado.Created_At,
                    CheckedInAt = convidado.CheckIn?.Created_at
                }).ToList()
            };
    }
}
