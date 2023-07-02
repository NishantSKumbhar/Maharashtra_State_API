using Microsoft.EntityFrameworkCore;
using PatanWalks.Models.Domain;

namespace PatanWalks.Data
{
    public class MaharashtraDbContext: DbContext
    {
        public MaharashtraDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
            // When we run entity core migrations, Below three properties will create tables in database.
            public DbSet<Population> Populations { get; set; }
            public DbSet<Division> Divisions { get; set; }
            public DbSet<District> Districts { get; set; }
   
    }
}
