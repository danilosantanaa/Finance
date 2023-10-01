using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.BancoDtos;
using Finance.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BancoController : ControllerBase
{
    private readonly IBancoService _bancoService;

    public BancoController(IBancoService bancoService)
    {
        _bancoService = bancoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var bancos = await _bancoService.GetAllAsync(User.GetId());

            return Ok(bancos);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Bancos. Problema: ${ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var banco = await _bancoService.GetByIdAsync(User.GetId(), id);
            if (banco is null) return NoContent();

            return Ok(banco);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Banco. Problema: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BancoDto model)
    {
        try
        {
            var banco = await _bancoService.AddAsync(User.GetId(), model);

            if (banco is null) return NoContent();

            return Ok(banco);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Banco. Problema: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] BancoDto model)
    {
        try
        {
            var banco = await _bancoService.UpdateAsync(User.GetId(), id, model);

            if (banco is null) return NoContent();

            return Ok(banco);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Banco. Problema: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var banco = await _bancoService.GetByIdAsync(User.GetId(), id);
            if (banco is null) return NoContent();

            var is_deleted = await _bancoService.DeleteAsync(User.GetId(), id);

            if (is_deleted)
            {
                return Ok(is_deleted.CreateResponseValueBoolean());
            }
            else
            {
                throw new ExceptionServiceBadRequestError("Ocorreu um problema não especifico ao tentar deletar Banco.");
            }

        }
        catch (ExceptionServiceBadRequestError ex)
        {
            return BadRequest(ex.CreateObjectExceptionResponse());
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Banco. Problema: {ex.Message}");
        }
    }
}