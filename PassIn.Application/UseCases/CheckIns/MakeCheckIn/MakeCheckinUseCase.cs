using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.MakeCheckIn;
public class MakeCheckinUseCase
{
    private readonly PassInDbContext _bancoSQL;
    public MakeCheckinUseCase()
        => _bancoSQL = new PassInDbContext();

    public ResponseRegisteredJson Execute(Guid atendeId)
    {
        Validate(atendeId);

        CheckIn entidade = new()
        {
            Attendee_Id = atendeId,
            Created_at = DateTime.UtcNow,
        };
        _bancoSQL.CheckIns.Add(entidade);
        _bancoSQL.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entidade.Id,
        };
    }

    private void Validate(Guid atendeId)
    {
        bool existePessoa = _bancoSQL.Attendees.Any(p => p.Id == atendeId);
        if (!existePessoa)
            throw new NotFoundException("Nao encontrado");

        bool existChekin = _bancoSQL.CheckIns.Any(check => check.Attendee_Id == atendeId);
        if (existePessoa)
            throw new ConflictException("Já Cadastrado neste evento");
    }
}
