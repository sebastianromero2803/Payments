using Microsoft.EntityFrameworkCore;
using Payments.Contracts.Repository;
using Payments.DataAccess.Context;
using Payments.Entities.Entities;
using System.Linq.Expressions;

namespace Payments.Repositories.ImplementRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MySqlContext _context;

        public PaymentRepository()
        {
            _context = new();
        }

        public async Task<Tuple<Payment, bool>> AddAsync(Payment entity)
        {
            try
            {
                var result = _context.Payments.Add(entity);
                await _context.SaveChangesAsync();
                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            try
            {
                var result = await _context.Payments.ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Payment>> GetByFilterAsync(Expression<Func<Payment, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.Payments.Where<Payment>(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Payments.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Tuple<Payment, bool>> UpdateAsync(Payment entity)
        {
            try
            {
                var result = _context.Payments.Update(entity);
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
