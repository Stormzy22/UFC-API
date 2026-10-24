using ApiUFC.Models;
using Microsoft.EntityFrameworkCore;
namespace ApiUFC.Data

{
    public class UfcDbContext : DbContext
    {
        public UfcDbContext(DbContextOptions<UfcDbContext> options)
            : base(options)
        { }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Nation> Nations { get; set; }
    }
}
