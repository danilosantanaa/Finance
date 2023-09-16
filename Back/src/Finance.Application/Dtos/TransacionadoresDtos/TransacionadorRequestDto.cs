using System.ComponentModel.DataAnnotations;
using Finance.Application.Attributes;
using Finance.Application.Dtos.EnderecosDtos;

namespace Finance.Application.Dtos.TransacionadoresDtos
{
    public class TransacionadorRequestDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.")]
        [FormatterString]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Deve ser um {0} no formato válido.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "O campo {0} não um número de telefone válido.")]
        public string Telefone { get; set; }

        public string TipoEnum { get; set; }
        public bool Status { get; set; }

        public EnderecoRequestDto EnderecoRequestDto { get; set; }

        public int CategoriaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

    }
}