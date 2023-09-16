using System.Security.Claims;
using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.IdentityDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Usuário: Problema: ${ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                {
                    return BadRequest("Usuário já se encontra em uso.");
                }

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user is not null)
                {
                    return Ok(user);
                }

                return BadRequest("Usuário não criado. Tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar registrar Usuário: Problema: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                const string MSG_ERRO_DEFAULT = "Usuário ou Senha Inválido.";

                var userResponseDto = await _accountService.GetUserByUserNameAsync(userLoginDto.UserName);
                if (userResponseDto is null) return Unauthorized(MSG_ERRO_DEFAULT);

                var result = await _accountService.CheckUserPasswordAsync(userLoginDto, userLoginDto.Password);

                if (!result.Succeeded)
                {
                    return Unauthorized(MSG_ERRO_DEFAULT);
                }

                return Ok(new
                {
                    PrimeiroNome = userResponseDto.PrimeiroNome,
                    Token = await _tokenService.CreateToken(userResponseDto)
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar o login: Problama: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                var userReturn = await _accountService.UpdateAccountAsync(userUpdateDto, User.GetId());

                if (userReturn is not null)
                {
                    return Ok(userReturn);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Usuário. Problema: {ex.Message}");
            }
        }
    }
}