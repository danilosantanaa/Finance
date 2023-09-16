using FinanceWasm.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FinanceWasm.Models {
    public class Categoria {
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string Descricao { get; set; }

        public bool Status { get; set; } = true;
    }
}
