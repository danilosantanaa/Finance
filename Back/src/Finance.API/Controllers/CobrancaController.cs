using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.CobrancaDtos;
using Finance.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;

        public CobrancaController(ICobrancaService cobrancaService)
        {
            _cobrancaService = cobrancaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cobrancas = await _cobrancaService.GetAllAsync(User.GetId());

                if (cobrancas is null) return NoContent();

                return Ok(cobrancas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cobrancas. Problema: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cobranca = await _cobrancaService.GetByIdAsync(User.GetId(), id);

                if (cobranca is null) return NoContent();

                return Ok(cobranca);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuparar cobrança por Id. Problema: {ex.Message}");
            }
        }

        [HttpGet("{id}/changeStatus/{status}")]
        public async Task<IActionResult> GetChangeStatus(int id, string status)
        {
            try
            {
                var is_modified = await _cobrancaService.UpdateStatusAsync(id, User.GetId(), status);

                return is_modified ? Ok(true) : BadRequest(new { statusChanged = false });
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                return BadRequest(ex.CreateObjectExceptionResponse());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar cobrança. Problema: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CobrancaDto model)
        {
            try
            {
                var cobranca = await _cobrancaService.AddAsync(User.GetId(), model);
                if (cobranca is null) return NoContent();

                return Ok(cobranca);
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                return BadRequest(ex.CreateObjectExceptionResponse());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar cobrança. Problema: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CobrancaDto model)
        {
            try
            {
                var cobranca = await _cobrancaService.UpdateAsync(User.GetId(), id, model);

                if (cobranca is null) NoContent();

                return Ok(cobranca);
            }
            catch (ExceptionServiceBadRequestError ex)
            {
                return BadRequest(ex.CreateObjectExceptionResponse());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar cobrança. Problema: {ex.Message}");
            }
        }
    }
}