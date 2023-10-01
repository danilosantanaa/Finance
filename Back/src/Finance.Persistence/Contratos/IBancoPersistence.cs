using Finance.Domain;

namespace Finance.Persistence.Contratos;

public interface IBancoPersistence : IGeneralPersistence
{
    Task<Banco> GetByIdAsync(int userId, int id);
    Task<Banco[]> GetAllAsync(int iserId);
}