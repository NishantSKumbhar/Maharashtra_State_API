using PatanWalks.Models.Domain;

namespace PatanWalks.Repositories
{
    public interface IDistrictRepository
    {
        // Repostory always deals with the Domain Model.
        Task<List<District>> GetAllDistrictsAsync();
        Task<District?> GetDistrictByIdAsync(Guid id);
        Task<District> PostDistrictAsync(District newDistrict);
        Task<District?> UpdateDistrictAsync(Guid id, District updatedDivision);
        Task<District?> DeleteDistrictAsync(Guid id);
    }
}
