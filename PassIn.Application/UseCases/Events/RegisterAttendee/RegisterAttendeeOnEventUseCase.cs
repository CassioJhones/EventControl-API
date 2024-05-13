using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _bancoSQL;
    public RegisterAttendeeOnEventUseCase() => _bancoSQL = new PassInDbContext();

    public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
    {

        Validate(eventId, request);
        Attendee entidade = new()
        {
            Email = request.Email,
            Name = request.Name,    
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,
        };

        _bancoSQL.Attendees.Add(entidade);
        _bancoSQL.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entidade.Id
        };
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        Event? eventExist = _bancoSQL.Events.Find(eventId);
        if (eventExist is null)
            throw new NotFoundException("Evento Nao existe");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("Titulo invalido");

        if (EmailValidator(request.Email) == false)
            throw new ErrorOnValidationException("Email invalido");

        bool attendeeExist = _bancoSQL.Attendees.Any(nome => nome.Email.Equals(request.Email));
        if (attendeeExist)
            throw new ConflictException("Cadastro ja existe");

        int totalAttendees = _bancoSQL.Attendees.Count(nome => nome.Event_Id == eventId);

        if (totalAttendees > eventExist.Maximum_Attendees)
            throw new ErrorOnValidationException("Espaço Insuficiente para tantas pessoas");

    }

    private bool EmailValidator(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
