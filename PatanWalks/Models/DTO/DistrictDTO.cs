using PatanWalks.Models.Domain;

namespace PatanWalks.Models.DTO
{
    public class DistrictDTO
    {
        // EXPOSE properties which you want to expose to client
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double AreaInSqKm { get; set; }
        public string? DistrictImageUrl { get; set; }
        public Guid DivisionId { get; set; }
        public Guid PopulationId { get; set; }
        // Navigation Property
        public Division Division { get; set; }
        public Population Population { get; set; }


    }
}
