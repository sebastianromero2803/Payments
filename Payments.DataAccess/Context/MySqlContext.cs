using Microsoft.EntityFrameworkCore;
using Payments.Entities.Entities;

namespace Payments.DataAccess.Context
{
    public class MySqlContext : DbContext
    {
        private readonly string _connectionString;

        public MySqlContext()
        {
            _connectionString = "server=localhost;uid=root;pwd=chachan2803;database=AdventFinal";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
