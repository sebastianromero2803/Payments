using Microsoft.EntityFrameworkCore;
using Payments.Contracts.Repository;
using Payments.DataAccess.Context;
using Payments.Entities.Entities;
using System.Linq.Expressions;

namespace Payments.Repositories.ImplementRepositories
{
    public class UserRepositoryMySql : IUserRepository
    {
        private readonly MySqlContext _context;

        public UserRepositoryMySql()
        {
            _context = new MySqlContext();
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var result = await _context.Users.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> GetByFilterAsync(Expression<Func<User, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.Users.Where(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var result = await _context.Users.FindAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tuple<User, bool>> UpdateAsync(User entity)
        {
            try
            {
                var result = _context.Users.Update(entity);
                await _context.SaveChangesAsync();
                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
