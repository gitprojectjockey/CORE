using Microsoft.EntityFrameworkCore;
using PostalService.Data.Entities;

namespace PostalService.Data
{
    public class GeoDataDbContext : DbContext
    {
        public GeoDataDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ZipCode> ZipCode { get; set; }
        public DbSet<State> State { get; set; }
    }
}
