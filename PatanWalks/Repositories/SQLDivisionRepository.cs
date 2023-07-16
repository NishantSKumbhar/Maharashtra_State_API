﻿using Microsoft.EntityFrameworkCore;
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

        
        public async Task<List<Division>> GetAllDivisionsAsync(string? filterOn = null, string? filterQuery = null)
        {
            // Make IQueryable
            var divisions = maharashtraDbContext.Divisions.AsQueryable();

            // Filter
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery))
            {
                // first check on which colum, you can make on different columns. 
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    divisions = divisions.Where(x => x.Name.Contains(filterQuery));
                }

                
            }
            return await divisions.ToListAsync();
            //return await maharashtraDbContext.Divisions.ToListAsync();
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
