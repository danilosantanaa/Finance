using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using FinanceWasm.Helpers;

namespace FinanceWasm.Models.Account
{
    public class Register
    {
        public int Id { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [Display(Name = "Último Nome")]
        public string UltimoNome { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [Compare(nameof(Password), ErrorMessage = "A senha não confere, tente novamente!")]
        public string ConfirmPassword { get; set; }
    }
}
