using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence.Persistences;

public class BancoPersistence : GeneralPersistence, IBancoPersistence
{
    private readonly FinanceContextDatabase _context;

    public BancoPersistence(FinanceContextDatabase context) : base(context)
    {
        _context = context;
    }

    public async Task<Banco[]> GetAllAsync(int userId)
    {
        IQueryable<Banco> query = _context.Bancos.AsNoTracking();

        query = query
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.Id);

        return await query.ToArrayAsync();
    }

    public async Task<Banco> GetByIdAsync(int userId, int id)
    {
        IQueryable<Banco> query = _context.Bancos.AsNoTracking();

        query = query
            .Where(b => b.UserId == userId)
            .OrderBy(b => b.Id);

        return await query.FirstOrDefaultAsync(b => b.Id == id);
    }
}