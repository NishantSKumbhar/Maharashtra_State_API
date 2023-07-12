using PatanWalks.Models.Domain;
using PatanWalks.Models.DTO;

namespace PatanWalks.Repositories
{
    public interface IDivisionRepository
    {
        Task<List<Division>> GetAllDivisionsAsync();
        Task<Division?> GetDivisionByIdAsync(Guid id);
        Task<Division> PostDivisionAsync(Division newDivision);
        Task<Division?> UpdateDivisionAsync(Guid id, Division updatedDivision);
        Task<Division?> DeleteDivisionAsync(Guid id);
    }
}
