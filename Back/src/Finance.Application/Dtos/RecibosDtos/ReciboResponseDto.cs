using Finance.Application.Dtos.BancoDtos;
using Finance.Application.Dtos.MovimentacaoDtos;

namespace Finance.Application.Dtos.RecibosDtos;

public class ReciboResponseDto
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public double Desconto { get; set; }
    public double Acrescimo { get; set; }
    public string Obs { get; set; }
    public int BancoId { get; set; }
    public BancoDto Banco { get; set; }
    public int CobrancaId { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; }
    public DateTime DataCancelamento { get; set; }
    public DateTime DataPagamento { get; set; }
    public int MovimentacaoId { get; set; }
    public MovimentacaoDto Movimentacao { get; set; }
}