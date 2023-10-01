using Finance.Domain;

namespace Finance.Persistence.Contratos;

public interface ICidadePersistence : IGeneralPersistence
{
    Task<Cidade[]> GetAllAsync();
}