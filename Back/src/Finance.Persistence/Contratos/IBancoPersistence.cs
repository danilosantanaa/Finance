using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain;

namespace Finance.Persistence.Contratos
{
    public interface IBancoPersistence : IGeneralPersistence
    {
        Task<Banco> GetByIdAsync(int userId, int id);
        Task<Banco[]> GetAllAsync(int iserId);
    }
}