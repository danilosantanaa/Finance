using System.ComponentModel.DataAnnotations;
using Finance.Application.Attributes;
using Finance.Application.Dtos.IdentityDto;

namespace Finance.Application.Dtos.BancoDtos
{
    public class BancoDto
    {
        public int Id { get; set; }

        [Required]
        [FormatterString]
        public string Nome { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
    }
}