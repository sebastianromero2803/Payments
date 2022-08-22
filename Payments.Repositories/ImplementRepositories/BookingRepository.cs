using Microsoft.EntityFrameworkCore;
using Payments.Contracts.Repository;
using Payments.DataAccess.Context;
using Payments.Entities.Entities;
using System.Linq.Expressions;

namespace Payments.Repositories.ImplementRepositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MySqlContext _context;

        public BookingRepository()
        {
            _context = new();
        }

        public async Task<Tuple<Booking, bool>> AddAsync(Booking entity)
        {
            try
            {
                var result = _context.Bookings.Add(entity);
                await _context.SaveChangesAsync();
                return Tuple.Create(result.Entity, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            try
            {
                var result = await _context.Bookings.ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Booking>> GetByFilterAsync(Expression<Func<Booking, bool>> expressionFilter = null)
        {
            try
            {
                return await _context.Bookings.Where(expressionFilter).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Bookings.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
