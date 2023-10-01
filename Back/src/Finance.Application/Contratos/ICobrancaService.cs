using Finance.Application.Dtos.CobrancaDtos;

namespace Finance.Application.Contratos;
public interface ICobrancaService
{
    Task<CobrancaDto> AddAsync(int userId, CobrancaDto model);
    Task<CobrancaDto> UpdateAsync(int userId, int id, CobrancaDto model);
    Task<CobrancaDto> GetByIdAsync(int userId, int id);
    Task<CobrancaDto[]> GetAllAsync(int userId);
    Task<bool> UpdateStatusAsync(int id, int userId, string status);
}