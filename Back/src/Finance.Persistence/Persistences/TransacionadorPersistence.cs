using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Persistences;

public class TransacionadorPersistence : GeneralPersistence, ITransacionadorPersistence
{
    private readonly FinanceContextDatabase _context;

    public TransacionadorPersistence(FinanceContextDatabase context) : base(context)
    {
        _context = context;
    }
    public async Task<Transacionador[]> GetAllAsync(int userId)
    {
        IQueryable<Transacionador> query = _context.Transacionadores.AsNoTracking();

        query = query.Include(t => t.Categoria);

        query = query
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Transacionador> GetByIdAsync(int userId, int id)
    {
        IQueryable<Transacionador> query = _context.Transacionadores.AsNoTracking();

        query = query
            .Where(t => t.Id == id && t.UserId == userId)
            .OrderBy(t => t.Id);

        return await query.FirstOrDefaultAsync();
    }
}