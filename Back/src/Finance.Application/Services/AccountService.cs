using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.IdentityDto;
using Finance.Application.Helpers;
using Finance.Domain.Identity;
using Finance.Persistence.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IUserPersistence _userPersistence;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserPersistence userPersistence)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _userPersistence = userPersistence;
    }

    public async Task<SignInResult> CheckUserPasswordAsync(UserLoginDto userLogin, string password)
    {
        try
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userLogin.UserName);

            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserResponseDto> CreateAccountAsync(UserDto userDto)
    {
        try
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserResponseDto>(user);
                return userToReturn;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserResponseDto> GetUserByUserNameAsync(string userName)
    {
        try
        {
            var user = await _userPersistence.GetUserByUserNameAsync(userName);
            if (user is null) return null;

            return _mapper.Map<UserResponseDto>(user);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserResponseDto> UpdateAccountAsync(UserUpdateDto userUpdateDto, int userId)
    {
        try
        {
            var user = await _userPersistence.GetUserByIdAsync(userId);
            if (user is null) return null;

            _mapper.Map(userUpdateDto, user);


            var userLoginDto = _mapper.Map<UserLoginDto>(user);
            if (!string.IsNullOrEmpty(userUpdateDto.OldPassword))
            {
                var signResult = await CheckUserPasswordAsync(userLoginDto, userUpdateDto.OldPassword);

                if (!signResult.Succeeded)
                    throw new ExceptionServiceBadRequestError("Senha Inválida!");

                if (string.IsNullOrEmpty(userUpdateDto.Password) || !userUpdateDto.Password.Equals(userUpdateDto.ConfirmPassword))
                    throw new ExceptionServiceBadRequestError("Senha de Confirmação não conferi.");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
            }

            _userPersistence.Update<User>(user);

            if (await _userPersistence.SaveChangesAsync())
            {
                var userRetorno = await _userPersistence.GetUserByUserNameAsync(user.UserName);

                return _mapper.Map<UserResponseDto>(userRetorno);
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UpdateFotoPerfil(string foto_Url, int userId)
    {
        try
        {
            var user = await _userPersistence.GetUserByIdAsync(userId);

            if (user is null) throw new ExceptionServiceBadRequestError("Usuário não encontrado.");

            user.ImagemPerfil = foto_Url;

            _userPersistence.Update(user);

            return await _userPersistence.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> UserExists(string userName)
    {
        try
        {
            return await _userManager.Users.AnyAsync(user => user.UserName == userName);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
