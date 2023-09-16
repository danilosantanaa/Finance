using Finance.Application.Dtos.IdentityDto;

namespace Finance.Application.Contratos
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserResponseDto userResponseDto);
    }
}