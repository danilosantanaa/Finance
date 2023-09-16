using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Domain.Identity;
using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Finance.Persistence
{
    public class UserPersistence : GeneralPersistence, IUserPersistence
    {
        private readonly FinanceContextDatabase _context;

        public UserPersistence(FinanceContextDatabase context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(usr => usr.Id == id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(usr => usr.UserName.Equals(userName));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}