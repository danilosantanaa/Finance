using Finance.Application.Dtos.IdentityDto;
using Microsoft.AspNetCore.Identity;

namespace Finance.Application.Contratos;

public interface IAccountService
{
    Task<bool> UserExists(string userName);
    Task<UserResponseDto> GetUserByUserNameAsync(string userName);
    Task<SignInResult> CheckUserPasswordAsync(UserLoginDto userLoginDto, string password);
    Task<UserResponseDto> CreateAccountAsync(UserDto userDto);
    Task<UserResponseDto> UpdateAccountAsync(UserUpdateDto userUpdateDto, int userId);
    Task<bool> UpdateFotoPerfil(string foto_Url, int userId);
}