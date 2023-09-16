using Finance.Domain;
using Finance.Persistence.Contratos;

namespace Finance.Persistence
{
    public interface ICidadePersistence : IGeneralPersistence
    {
        Task<Cidade[]> GetAllAsync();
    }
}