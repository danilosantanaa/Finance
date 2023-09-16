using FinanceWasm.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinanceWasm.Models
{

    public class Transacionador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = ModelMessageErro.EMAIL_VALID)]
        public string Email { get; set; }

        [Display(Name ="Telefone")]
        [Phone(ErrorMessage = ModelMessageErro.PHONE_VALID)]
        public string Telefone { get; set; }

        public bool Status { get; set; } = true;

        public IEnumerable<Cobranca> Cobranca { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = ModelMessageErro.REQUIRED)]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}