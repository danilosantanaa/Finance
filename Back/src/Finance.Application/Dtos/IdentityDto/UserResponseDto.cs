namespace Finance.Application.Dtos.IdentityDto;

public class UserResponseDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PrimeiroNome { get; set; }
    public string UltimoNome { get; set; }
    public string PhoneNumber { get; set; }
    public string Descricao { get; set; }
    public string ImagemPerfil { get; set; }
}