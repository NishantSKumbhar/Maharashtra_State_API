using PatanWalks.Models.Domain;

namespace PatanWalks.Repositories
{
    public interface IDivisionRepository
    {
        Task<List<Division>> GetAllAsyncDivisions();
    }
}
