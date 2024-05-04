using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _bancoSQL;
    public RegisterAttendeeOnEventUseCase() => _bancoSQL = new PassInDbContext();

    public void Execute(Guid eventId, RequestRegisterEventJson request)
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
    }

    private void Validate(Guid eventId, RequestRegisterEventJson request)
    {
        bool eventExist = _bancoSQL.Events.Any(evento => evento.Id == eventId);
        if (!eventExist)
            throw new NotFoundException("Evento Nao existe");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ErrorOnValidationException("Titulo invalido");

        if (EmailValidator(request.Email) == false)
            throw new ErrorOnValidationException("Email invalido");

        bool attendeeExist = _bancoSQL.Attendees.Any(nome => nome.Email.Equals(request.Email));
        if (attendeeExist)
            throw new ErrorOnValidationException("Cadastro ja existe");
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
