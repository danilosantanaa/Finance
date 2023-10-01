using Microsoft.AspNetCore.Identity;

namespace Finance.Domain.Identity;

public class User : IdentityUser<int>
{
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public string ImagemPerfil { get; set; }
    public bool Ativo { get; set; }
    public IEnumerable<UserRole> UserRoles { get; set; }

    public IEnumerable<Banco> Bancos { get; set; }
    public IEnumerable<Cobranca> Cobrancas { get; set; }
    public IEnumerable<Transacionador> Transacionadores { get; set; }
    public IEnumerable<Recibo> Recibos { get; set; }
    public IEnumerable<Categoria> Categorias { get; set; }
}