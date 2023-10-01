using Finance.Application.Attributes;

namespace Finance.Application.Dtos.IdentityDto;

public class UserDto
{
    [TreatSpaces(WithoutSpace = true)]
    public string UserName { get; set; }

    [TreatSpaces]
    public string PrimeiroNome { get; set; }
    [TreatSpaces]
    public string UltimoNome { get; set; }
    public string PhoneNumber { get; set; }

    [TreatSpaces]
    public string Descricao { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}