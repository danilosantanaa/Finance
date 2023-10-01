using Finance.Domain;

namespace Finance.Persistence.Contratos;

public interface IEnderecoPersistence : IGeneralPersistence
{
    Task<Endereco[]> GetAllAsync(int userId, bool includeRegiao = true);
}