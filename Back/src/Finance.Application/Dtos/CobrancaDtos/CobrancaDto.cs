using System.ComponentModel.DataAnnotations;
using Finance.Application.Attributes;
using Finance.Application.Dtos.IdentityDto;
using Finance.Application.Dtos.RecibosDtos;
using Finance.Domain;

namespace Finance.Application.Dtos.CobrancaDtos
{
    public class CobrancaDto
    {
        public int Id { get; set; }

        [Required]
        [FormatterString]
        public string Descricao { get; set; }
        public int QuantidadeParcelas { get; set; }
        public string Tipo { get; set; }

        [Required]
        public string TipoCobranca { get; set; }
        public string Status { get; set; }
        public double Valor { get; set; }
        public DateTime DataEmissao { get; set; }

        [Required]
        public int TransacionadorId { get; set; }
        public Transacionador Transacionador { get; set; }

        public IEnumerable<ReciboResponseDto> Recibos { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}