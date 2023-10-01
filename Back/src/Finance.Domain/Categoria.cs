using System.ComponentModel.DataAnnotations;

namespace Finance.Domain;

public class Categoria : Entity
{
    [Required]
    public string Descricao { get; set; }
    public bool Status { get; set; }
    public int UserId { get; set; }

    public IEnumerable<Transacionador> Transacionadores { get; set; }
}
