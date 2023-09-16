using AutoMapper;
using Finance.Application.Contratos;
using Finance.Application.Dtos.IdentityDto;
using Finance.Domain.Identity;
using Finance.Persistence.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application
{
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
                throw new Exception($"Erro ao tentar verificar senha. Erro: {ex.Message}");
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
                throw new Exception($"Erro ao tentar Criar Usuário. Erro: {ex.Message}");
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
                throw new Exception($"Erro ao tentar pegar Usuário por Username: {ex.Message}");
            }
        }

        public async Task<UserResponseDto> UpdateAccountAsync(UserUpdateDto userUpdateDto, int userId)
        {
            try
            {
                var user = await _userPersistence.GetUserByIdAsync(userId);
                if (user is null) return null;

                _mapper.Map(userUpdateDto, user);

                //TODO: Implementar a funcionalidade de se caso preencher a nova senha e não bater, lançar um erro.
                // Se caso a senha antiga não bater, lançar uma exceção também.
                if (!string.IsNullOrEmpty(userUpdateDto.Password) && userUpdateDto.ConfirmPassword.Equals(userUpdateDto.ConfirmPassword))
                {
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
                throw new Exception($"Erro ao tentar Atualizar Usuário. Erro: {ex.Message}");
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
}