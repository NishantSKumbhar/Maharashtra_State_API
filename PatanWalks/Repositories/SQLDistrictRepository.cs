using Microsoft.EntityFrameworkCore;
using PatanWalks.Data;
using PatanWalks.Models.Domain;

namespace PatanWalks.Repositories
{
    public class SQLDistrictRepository : IDistrictRepository
    {
        private readonly MaharashtraDbContext maharashtraDbContext;
        public SQLDistrictRepository(MaharashtraDbContext maharashtraDbContext)
        {
            this.maharashtraDbContext = maharashtraDbContext;
        }
        public Task<District?> DeleteDistrictAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<District>> GetAllDistrictsAsync()
        {
            return await maharashtraDbContext.Districts.ToListAsync();
        }

        public async Task<District?> GetDistrictByIdAsync(Guid id)
        {
            return await maharashtraDbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<District> PostDistrictAsync(District newDistrict)
        {
            await maharashtraDbContext.Districts.AddAsync(newDistrict);
            await maharashtraDbContext.SaveChangesAsync();
            return newDistrict;
        }

        public Task<District?> UpdateDistrictAsync(Guid id, District updatedDivision)
        {
            throw new NotImplementedException();
        }
    }
}
