using Microsoft.EntityFrameworkCore;
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
        Event? participantes = _bancoSQL.Events
            .Include(ev => ev.Attendees)
            .FirstOrDefault(ev => ev.Id == eventId);
        if (participantes is null)
            throw new NotFoundException("Nao Existe");

        return new ResponseAllAttendeesJson
        {
            Attendees = participantes.Attendees.Select(at=> new ResponseAttendeeJson
            {
                Id = at.Id,
                Name = at.Name,
                Email = at.Email,
                CreatedAt  = at.Created_At
            }).ToList()
        };
    }
}
