using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.CategoriaDtos;
using Finance.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;
    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var categorias = await _categoriaService.GetAllAsync(User.GetId());

            return Ok(categorias);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Categorias. Problema: ${ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var categoria = await _categoriaService.GetByIdAsync(User.GetId(), id);

            if (categoria is null) return NoContent();

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Categoria. Problema: ${ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CategoriaDto model)
    {
        try
        {
            var categoria = await _categoriaService.AddAsync(User.GetId(), model);

            if (categoria is null) return NoContent();

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Categoria. Problema: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CategoriaDto model)
    {
        try
        {
            var categoria = await _categoriaService.UpdateAsync(User.GetId(), id, model);

            if (categoria is null) return NoContent();

            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Categoria. Problema: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var categoria = await _categoriaService.GetByIdAsync(User.GetId(), id);
            if (categoria is null) return NoContent();

            var is_deleted = await _categoriaService.DeleteAsync(User.GetId(), id);

            if (is_deleted)
            {
                return Ok(is_deleted.CreateResponseValueBoolean());
            }
            else
            {
                throw new ExceptionServiceBadRequestError("Ocorreu um problema não especifico ao tentar deletar Categoria.");
            }
        }
        catch (ExceptionServiceBadRequestError ex)
        {
            return BadRequest(ex.CreateObjectExceptionResponse());
        }
        catch (Exception ex)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Categoria. Problema: {ex.Message}");
        }
    }
}