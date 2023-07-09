using Microsoft.EntityFrameworkCore;
using PatanWalks.Models.Sample;

namespace PatanWalks.Data
{
    public class MobileDbContext: DbContext
    {
        public MobileDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // Mobile Table
        public DbSet<MobileModel> Mobile { get; set; }
    }
}
