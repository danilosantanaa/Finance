using System.ComponentModel.DataAnnotations;
using Finance.Application.Dtos.BancoDtos;
using Finance.Domain.Enum;

namespace Finance.Application.Dtos.RecibosDtos
{
    public class ReciboDto
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public double Acrescimo { get; set; }
        public string Obs { get; set; }

        [Required]
        public int BancoId { get; set; }
        public ReciboStatusEnum Status { get; set; }
        public BancoDto Banco { get; set; }

        [Required]
        public int CobrancaId { get; set; }
    }
}