using Finance.Domain;

namespace Finance.Persistence.Contratos
{
    public interface ITransacionadorPersistence : IGeneralPersistence
    {
        Task<Transacionador> GetByIdAsync(int userId, int id);
        Task<Transacionador[]> GetAllAsync(int userId);
    }
}