using FinanceWasm.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinanceWasm.Models.Account {
    public class Account {
        public int Id { get; set; }

        [Display(Name = "Usuário")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [Display(Name = "Primeiro Nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = ModelMessageErro.REQUIRED)]
        [Display(Name = "Último Nome")]
        public string UltimoNome { get; set; }
        public string PhoneNumber { get; set; }
        public string Descricao { get; set; }

        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }

        [Display(Name = "Senha Antiga")]
        public string OldPassword { get; set; }

        public string ImagemPerfil { get; set; }
    }
}
