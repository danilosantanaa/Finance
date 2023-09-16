using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence
{
    public class ReciboPersistence : GeneralPersistence, IReciboPersistence
    {
        private readonly FinanceContextDatabase _context;

        public ReciboPersistence(FinanceContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<Recibo[]> GetAllAsync(int userId, int cobrancaId, bool includeCobranca = false)
        {
            IQueryable<Recibo> query = _context.Recibos
                .AsNoTracking()
                .Include(x => x.Banco)
                .Include(x => x.Movimentacao);

            if (includeCobranca)
            {
                query = query.Include(c => c.Cobranca);
            }

            query = query
                .Where(r => r.CobrancaId == cobrancaId && r.UserId == userId)
                .OrderByDescending(r => r.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Recibo> GetByIdAsync(int userId, int cobrancaId, int reciboId, bool includeCobranca = false)
        {
            IQueryable<Recibo> query = _context.Recibos
                .AsNoTracking()
                .Include(r => r.Banco)
                .Include(x => x.Movimentacao);

            if (includeCobranca)
            {
                query = query.Include(c => c.Cobranca);
            }

            query = query
                .Where(r => r.UserId == userId && r.CobrancaId == cobrancaId && r.Id == reciboId);

            return await query.SingleOrDefaultAsync();
        }
    }
}