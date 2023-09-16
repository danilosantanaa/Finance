using Finance.Domain;

namespace Finance.Persistence.Contratos
{
    public interface ICategoriaPersistence : IGeneralPersistence
    {
        Task<Categoria> GetByIdAsync(int userId, int id);
        Task<Categoria[]> GetAllAsync(int userId);
    }
}