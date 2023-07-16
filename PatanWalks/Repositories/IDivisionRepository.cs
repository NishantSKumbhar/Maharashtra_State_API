﻿using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;

namespace PatanWalks.Repositories
{
    public interface IDivisionRepository
    {
        // bydefault null
        Task<List<Division>> GetAllDivisionsAsync(string? filterOn = null, string? filterQuery= null);
        Task<Division?> GetDivisionByIdAsync(Guid id);
        Task<Division> PostDivisionAsync(Division newDivision);
        Task<Division?> UpdateDivisionAsync(Guid id, Division updatedDivision);
        Task<Division?> DeleteDivisionAsync(Guid id);
    }
}
