using Finance.Application.Dtos.CategoriaDtos;

namespace Finance.Application.Contratos;

public interface ICategoriaService
{
    Task<CategoriaDto> AddAsync(int userId, CategoriaDto model);
    Task<CategoriaDto> UpdateAsync(int userId, int id, CategoriaDto model);
    Task<bool> DeleteAsync(int userId, int id);

    Task<CategoriaDto[]> GetAllAsync(int userId);
    Task<CategoriaDto> GetByIdAsync(int userId, int id);
}
