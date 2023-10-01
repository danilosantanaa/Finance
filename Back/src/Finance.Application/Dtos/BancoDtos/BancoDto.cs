using System.ComponentModel.DataAnnotations;
using Finance.Application.Attributes;

namespace Finance.Application.Dtos.BancoDtos;

public class BancoDto
{
    public int Id { get; set; }

    [Required]
    [TreatSpaces]
    public string Nome { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataUltimaAlteracao { get; set; }
}
