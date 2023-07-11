using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;

namespace PatanWalks.Repositories
{
    public class SQLDivisionRepository : IDivisionRepository
    {
        private readonly MaharashtraDbContext maharashtraDbContext;
        public SQLDivisionRepository(MaharashtraDbContext maharashtraDbContext)
        {
            this.maharashtraDbContext = maharashtraDbContext;
        }
        public async Task<List<Division>> GetAllAsyncDivisions()
        {
            return await maharashtraDbContext.Divisions.ToListAsync();
        }
    }
}
