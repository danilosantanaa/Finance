using Finance.Domain;

namespace Finance.Persistence.Contratos
{
    public interface ICobrancaPersistence : IGeneralPersistence
    {
        Task<Cobranca> GetByIdAsync(int userId, int id, bool includeRecibos = false);
        Task<Cobranca[]> GetAllAsync(int userId);
    }
}