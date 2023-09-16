using FinanceWasm.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FinanceWasm.Models.Account {
    public class Login {

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string Password { get; set; }
    }
}
