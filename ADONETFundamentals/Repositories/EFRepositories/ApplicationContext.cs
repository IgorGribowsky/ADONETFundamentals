using ADONETFundamentals.Constants;
using ADONETFundamentals.Models;
using Microsoft.EntityFrameworkCore;

namespace ADONETFundamentals.Repositories.EFRepositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConstants.ConnectionString);
        }
    }
}
