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

        
        public async Task<List<Division>> GetAllDivisionsAsync()
        {
            return await maharashtraDbContext.Divisions.ToListAsync();
        }

        public async Task<Division?> GetDivisionByIdAsync(Guid id)
        {
            return await maharashtraDbContext.Divisions.FindAsync(id);
        }

        public async Task<Division> PostDivisionAsync(Division newDivision)
        {
            await maharashtraDbContext.AddAsync(newDivision);
            await maharashtraDbContext.SaveChangesAsync();
            return newDivision; 
        }

        public async Task<Division?> UpdateDivisionAsync(Guid id, Division updatedDivision)
        {
            var existingDivision = await maharashtraDbContext.Divisions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDivision == null)
            {
                return null;
            }

            existingDivision.Code = updatedDivision.Code;
            existingDivision.Name = updatedDivision.Name;
            existingDivision.DivisionImageUrl = updatedDivision.DivisionImageUrl;

            await maharashtraDbContext.SaveChangesAsync();

            return existingDivision;

        }
        public async Task<Division?> DeleteDivisionAsync(Guid id)
        {
            var existingDivision = await maharashtraDbContext.Divisions.FirstOrDefaultAsync(x => x.Id == id);
            
            if (existingDivision == null)
            {
                return null;
            }
            
            maharashtraDbContext.Remove(existingDivision);
            
            await maharashtraDbContext.SaveChangesAsync();
            
            return existingDivision;
        }
    }
}
