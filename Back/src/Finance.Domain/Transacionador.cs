using System.ComponentModel.DataAnnotations;
using Finance.Domain.Identity;

namespace Finance.Domain;

public class Transacionador : Entity
{
    [Required]
    public string Nome { get; set; }

    public string Email { get; set; }

    public string Telefone { get; set; }

    public bool Status { get; set; }

    public IEnumerable<Cobranca> Cobrancas { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }

    public Endereco Endereco { get; set; }
}