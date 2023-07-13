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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the Data For the Populations
            // id, count of people
            // Guid.NewGuid() in C# interacive environment.
            var populations = new List<Population>()
            {
                new Population()
                {
                    Id = Guid.Parse("771ee197-0f61-4fbf-994b-9772393fbbdc"),
                    CountOfPeople = 71820
                },
                new Population()
                {
                    Id = Guid.Parse("45364c4e-4b50-4c58-9c12-ca95e3af1c18"),
                    CountOfPeople = 56374
                },
                new Population()
                {
                    Id = Guid.Parse("482eba9f-42b0-452a-9b1d-5b16c9b97228"),
                    CountOfPeople = 10393
                }
            };

            modelBuilder.Entity<Population>().HasData(populations);
            // after that we have to migrate

        }

    }
}
