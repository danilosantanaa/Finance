using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;

namespace Finance.Persistence.Persistences;

public class GeneralPersistence : IGeneralPersistence
{
    private readonly FinanceContextDatabase _context;

    public GeneralPersistence(FinanceContextDatabase context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public void DeleteRange<T>(T[] entities) where T : class
    {
        _context.RemoveRange(entities);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}