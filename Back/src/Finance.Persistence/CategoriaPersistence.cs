using Finance.Domain;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence
{
    public class CategoriaPersistence : GeneralPersistence, ICategoriaPersistence
    {
        private readonly FinanceContextDatabase _context;

        public CategoriaPersistence(FinanceContextDatabase context) : base(context)
        {
            _context = context;
        }
        public async Task<Categoria[]> GetAllAsync(int userId)
        {
            IQueryable<Categoria> query = _context.Categorias.AsNoTracking();

            query = query.Where(c => c.UserId == userId);

            return await query.ToArrayAsync();
        }

        public async Task<Categoria> GetByIdAsync(int userId, int id)
        {
            IQueryable<Categoria> query = _context.Categorias.AsNoTracking();

            query = query.Where(c => c.UserId == userId && c.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}