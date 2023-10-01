using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.TransacionadoresDtos;
using Finance.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransacionadorController : ControllerBase
{
    private readonly ITransacionadorService _transacionadorService;

    public TransacionadorController(ITransacionadorService transacionadorService)
    {
        _transacionadorService = transacionadorService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var transacionadores = await _transacionadorService.GetAllAsync(User.GetId());

            if (transacionadores is null) return NoContent();

            return Ok(transacionadores);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Transacionadores. Problema: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var transacionador = await _transacionadorService.GetByIdAsync(User.GetId(), id);

            if (transacionador is null) return NoContent();

            return Ok(transacionador);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Transacionador. Problema: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TransacionadorRequestDto model)
    {
        try
        {
            var transacionador = await _transacionadorService.AddAsync(User.GetId(), model);

            if (transacionador is null) return NoContent();

            return Ok(transacionador);
        }
        catch (ExceptionServiceBadRequestError ex)
        {
            return BadRequest(ex.CreateObjectExceptionResponse());
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Transacionador. Problema: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TransacionadorRequestDto model)
    {
        try
        {
            var transacionador = await _transacionadorService.UpdateAsync(User.GetId(), id, model);

            if (transacionador is null) return NoContent();

            return Ok(transacionador);
        }
        catch (ExceptionServiceBadRequestError ex)
        {
            return BadRequest(ex.CreateObjectExceptionResponse());
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Transacionador. Problema: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var transacionador = await _transacionadorService.GetByIdAsync(User.GetId(), id);
            if (transacionador is null) return NoContent();

            if (await _transacionadorService.DeleteAsync(User.GetId(), id))
            {
                return Ok("Deletado");
            }
            else
            {
                throw new Exception("Ocorreu um problema não específico ao tentar deletar Transacionador");
            }
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Transacionador. Problema: {ex.Message}");
        }
    }
}