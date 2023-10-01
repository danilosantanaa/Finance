using Finance.Domain.Identity;

namespace Finance.Persistence.Contratos;

public interface IUserPersistence : IGeneralPersistence
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUserNameAsync(string userName);
}