using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Persistences;

public class CobrancaPersistence : GeneralPersistence, ICobrancaPersistence
{
    private readonly FinanceContextDatabase _context;

    public CobrancaPersistence(FinanceContextDatabase context) : base(context)
    {
        _context = context;
    }
    public async Task<Cobranca[]> GetAllAsync(int userId)
    {
        IQueryable<Cobranca> query = _context.Cobrancas.AsNoTracking();

        query = query
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Id);

        return await query.ToArrayAsync();
    }

    public Task<Cobranca> GetByIdAsync(int userId, int id, bool includeRecibos = false)
    {
        IQueryable<Cobranca> query = _context.Cobrancas.AsNoTracking();

        if (includeRecibos)
        {
            query = query
                .Include(c => c.Recibos.OrderByDescending(r => r.Id))
                .ThenInclude(r => r.Banco);
        }

        query = query.
            OrderBy(c => c.Id)
            .Where(c => c.Id == id && c.UserId == userId);

        return query.FirstOrDefaultAsync();
    }
}