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
        public async Task<District?> DeleteDistrictAsync(Guid id)
        {
            var district = await maharashtraDbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            maharashtraDbContext.Districts.Remove(district);
            await maharashtraDbContext.SaveChangesAsync();
            return district;
        }

        public async Task<List<District>> GetAllDistrictsAsync()
        {
            //return await maharashtraDbContext.Districts.ToListAsync();
            // above we only get the id of the table, see the domain model , Navigation Properties.
            return await maharashtraDbContext.Districts.Include("Division").Include("Population").ToListAsync();
        }

        public async Task<District?> GetDistrictByIdAsync(Guid id)
        {
            return await maharashtraDbContext.Districts.Include("Division").Include("Population").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<District> PostDistrictAsync(District newDistrict)
        {
            await maharashtraDbContext.Districts.AddAsync(newDistrict);
            await maharashtraDbContext.SaveChangesAsync();
            return newDistrict;
        }

        public async Task<District?> UpdateDistrictAsync(Guid id, District updatedDistrict)
        {
            var existingDistrict = await maharashtraDbContext.Districts.FirstOrDefaultAsync(x => x.Id == id);
            if(existingDistrict == null)
            {
                return null;
            }

            existingDistrict.Name = updatedDistrict.Name;
            existingDistrict.Description = updatedDistrict.Description;
            existingDistrict.AreaInSqKm = updatedDistrict.AreaInSqKm;
            existingDistrict.DistrictImageUrl = updatedDistrict.DistrictImageUrl;
            existingDistrict.PopulationId = updatedDistrict.PopulationId;
            existingDistrict.DivisionId = updatedDistrict.DivisionId;

            await maharashtraDbContext.SaveChangesAsync();
            return existingDistrict;
        }
    }
}
