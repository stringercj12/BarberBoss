using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess
{
    public class BarberBossDbContext : DbContext
    {

        public BarberBossDbContext(DbContextOptions options) : base(options)
        {
            
        }

       public DbSet<Invoicing> Invoicings { get; set; }

    }
}
