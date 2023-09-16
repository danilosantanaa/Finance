using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence
{
    public class EnderecoPersistence : GeneralPersistence, IEnderecoPersistence
    {
        private readonly FinanceContextDatabase _context;

        public EnderecoPersistence(FinanceContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<Endereco[]> GetAllAsync(int userId, bool includeRegiao = true)
        {
            IQueryable<Endereco> query = _context.Enderecos.AsNoTracking();

            if (includeRegiao)
            {
                query = query.Include(c => c.Cidade).ThenInclude(e => e.Estado);
            }

            return await query.ToArrayAsync();
        }
    }
}