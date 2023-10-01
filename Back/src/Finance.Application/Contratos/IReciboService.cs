using Finance.Application.Dtos.RecibosDtos;

namespace Finance.Application.Contratos;
public interface IReciboService
{
    Task<ReciboDto[]> SaveRecibos(int userId, int cobrancaId, ReciboDto[] models);
    Task<bool> CancelarRecibo(int userId, int cobrancaId, int reciboId);
    Task<ReciboResponseDto[]> GetAllAsync(int userId, int cobrancaId);
    Task<ReciboResponseDto> GetByIdAsync(int userId, int cobrancaId, int reciboId);
}