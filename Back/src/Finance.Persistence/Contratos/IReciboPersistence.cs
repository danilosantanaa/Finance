using Finance.Domain;

namespace Finance.Persistence.Contratos;

public interface IReciboPersistence : IGeneralPersistence
{
    Task<Recibo[]> GetAllAsync(int userId, int cobrancaId, bool includeCobranca = false);
    Task<Recibo> GetByIdAsync(int userId, int cobrancaId, int reciboId, bool includeCobranca = false);
}