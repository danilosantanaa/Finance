using Finance.Application.Dtos.BancoDtos;

namespace Finance.Application.Contratos
{
    public interface IBancoService
    {
        Task<BancoDto> AddAsync(int userId, BancoDto model);
        Task<BancoDto> UpdateAsync(int userId, int bancoId, BancoDto model);
        Task<bool> DeleteAsync(int userId, int bancoId);
        Task<BancoDto> GetByIdAsync(int userId, int id);
        Task<BancoDto[]> GetAllAsync(int userId);
    }
}