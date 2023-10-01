using Finance.Application.Attributes;

namespace Finance.Application.Dtos.IdentityDto;

public class UserLoginDto
{
    [TreatSpaces]
    public string UserName { get; set; }
    public string Password { get; set; }
}