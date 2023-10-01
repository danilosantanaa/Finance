using Finance.Application.Dtos.TransacionadoresDtos;

namespace Finance.Application.Contratos;

public interface ITransacionadorService
{
    Task<TransacionadorResponseDto> AddAsync(int userId, TransacionadorRequestDto model);
    Task<TransacionadorResponseDto> UpdateAsync(int userId, int id, TransacionadorRequestDto model);
    Task<bool> DeleteAsync(int userId, int id);
    Task<TransacionadorResponseDto> GetByIdAsync(int userId, int id);
    Task<TransacionadorResponseDto[]> GetAllAsync(int userId);
}