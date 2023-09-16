using Finance.Application.Dtos.RecibosDtos;

namespace Finance.Application.Dtos.MovimentacaoDtos
{
    public class MovimentacaoDto
    {
        public int ReciboId { get; set; }
        public ReciboResponseDto Recibo { get; set; }
        public DateTime DataMovimentacao { get; set; } = new();
    }
}