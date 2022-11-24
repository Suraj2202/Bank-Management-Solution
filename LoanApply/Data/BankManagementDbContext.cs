using LoanApply.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace LoanApply.Data
{
    public class BankManagementDbContext : DbContext
    {
        public BankManagementDbContext(DbContextOptions<BankManagementDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }

    }
}
