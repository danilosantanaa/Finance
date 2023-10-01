using Finance.Domain.Enum;
using Finance.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace Finance.Domain;

public class Cobranca : Entity
{
    [Required]
    public string Descricao { get; set; }
    public int QuantidadeParcelas { get; set; }

    [Required]
    public TipoEnum Tipo { get; set; }

    [Required]
    public TipoCobrancaEnum TipoCobranca { get; set; }

    public CobrancaStatusEnum Status { get; set; } = CobrancaStatusEnum.Lancado;
    public double Valor { get; set; }
    public DateTime DataEmissao { get; set; } = DateTime.Now;

    public int TransacionadorId { get; set; }
    public Transacionador Transacionador { get; set; }

    public IEnumerable<Recibo> Recibos { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public double GetValorTotal() => Valor;
    public double GetValorMensal() =>
        TipoCobranca == TipoCobrancaEnum.Mensal && QuantidadeParcelas > 0 ?
        Math.Round(Valor / QuantidadeParcelas, 2) : 0;
}