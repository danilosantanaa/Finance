using System.ComponentModel.DataAnnotations;
using Finance.Domain.Enum;
using Finance.Domain.Identity;

namespace Finance.Domain;

public class Recibo : Entity
{
    public Recibo()
    {
        Movimentacao = new();
        DataPagamento = DateTime.Now;
        Status = ReciboStatusEnum.Ativo;
    }

    private double _valor;
    public double Valor
    {
        get => _valor;
        set => _valor = Math.Abs(value);
    }
    public double Desconto { get; set; }
    public double Acrescimo { get; set; }
    public string Obs { get; set; }

    private ReciboStatusEnum _status;
    public ReciboStatusEnum Status
    {
        get => _status;
        set
        {
            _status = value;

            if (value == ReciboStatusEnum.Cancelado && DataCancelamento == DateTime.MinValue)
            {
                DataCancelamento = DateTime.Now;
            }
        }
    }

    public DateTime DataPagamento { get; set; }
    public DateTime DataCancelamento { get; set; }

    [Required]
    public int BancoId { get; set; }
    public Banco Banco { get; set; }

    [Required]
    public int CobrancaId { get; set; }
    public Cobranca Cobranca { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    public int MovimentacaoId { get; set; }
    public Movimentacao Movimentacao { get; set; }

    public double GetValorTotal() => Valor + Acrescimo - Desconto;
}