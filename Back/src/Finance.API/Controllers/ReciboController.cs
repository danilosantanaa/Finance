using Finance.API.Extensions;
using Finance.Application.Contratos;
using Finance.Application.Dtos.RecibosDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReciboController : ControllerBase
    {
        private readonly IReciboService _reciboService;

        public ReciboController(IReciboService reciboService)
        {
            _reciboService = reciboService;
        }

        [HttpGet("{cobrancaId}")]
        public async Task<IActionResult> Get(int cobrancaId)
        {
            try
            {
                var recibos = await _reciboService.GetAllAsync(User.GetId(), cobrancaId);

                if (recibos is null) return NoContent();

                return Ok(recibos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar Recibo. Problema: {ex.Message}");
            }
        }

        [HttpGet("{cobrancaId}/{reciboId}")]
        public async Task<IActionResult> GetById(int cobrancaId, int reciboId)
        {
            try
            {
                var recibo = await _reciboService.GetByIdAsync(User.GetId(), cobrancaId, reciboId);

                if (recibo is null) return NoContent();

                return Ok(recibo);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar Recibo. Problema: {ex.Message}");
            }
        }

        [HttpPut("{cobrancaId}")]
        public async Task<IActionResult> SaveRecibos(int cobrancaId, ReciboDto[] models)
        {
            try
            {
                var recibos = await _reciboService.SaveRecibos(User.GetId(), cobrancaId, models);
                if (recibos is null) NoContent();

                return Ok(recibos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar Recibos. Problema: {ex.Message}");
            }
        }

        [HttpGet("{cobrancaId}/{reciboId}/cancelar")]
        public async Task<IActionResult> CancelarRecibo(int cobrancaId, int reciboId)
        {
            try
            {
                var recibo = await _reciboService.GetByIdAsync(User.GetId(), cobrancaId, reciboId);
                if (recibo is null) return NoContent();

                var cancelado = await _reciboService.CancelarRecibo(User.GetId(), cobrancaId, reciboId);

                if (!cancelado) throw new Exception("Houve um problema ao Cancelar Recibo. Tente novamente mais tarde.");

                return Ok(new
                {
                    title = cancelado
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Cancelar Recibo. Problema: {ex.Message}.");
            }
        }
    }
}