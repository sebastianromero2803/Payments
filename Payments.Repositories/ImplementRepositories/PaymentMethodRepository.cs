using Microsoft.EntityFrameworkCore;
using Payments.Contracts.Repository;
using Payments.DataAccess.Context;
using Payments.Entities.Entities;
using System.Linq.Expressions;

namespace Payments.Repositories.ImplementRepositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly MySqlContext _context;
        public PaymentMethodRepository()
        {
            _context = new();
        }

        public Task<List<PaymentMethod>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentMethod>> GetByFilterAsync(Expression<Func<PaymentMethod, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.PaymentMethods.Where(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentMethod> GetByIdAsync(int id)
        {
            try
            {
                return await _context.PaymentMethods.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
