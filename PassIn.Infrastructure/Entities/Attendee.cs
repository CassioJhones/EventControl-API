using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure.Entities;
public class Attendee
{// Classe referente a Tabela Attendee no Banco de Dados
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid Event_Id { get; set; }
    public DateTime Created_At { get; set; }
    [ForeignKey("CheckIn_Id")]
    public CheckIn? CheckIn { get; set; }
}
