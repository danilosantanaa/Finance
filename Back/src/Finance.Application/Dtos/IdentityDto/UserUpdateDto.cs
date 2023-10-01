using System.ComponentModel.DataAnnotations;

namespace Finance.Application.Dtos.IdentityDto;

public class UserUpdateDto
{

    [Required]
    public string PrimeiroNome { get; set; }

    [Required]
    public string UltimoNome { get; set; }
    public string PhoneNumber { get; set; }
    public string Descricao { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string OldPassword { get; set; }
    public string Token { get; set; }
}