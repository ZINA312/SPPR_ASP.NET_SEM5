using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.Domain;
using WEB_253503_Kudosh.Domain.Entities;

namespace WEB_253503_Kudosh.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TelescopeEntity> Telescopes { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
