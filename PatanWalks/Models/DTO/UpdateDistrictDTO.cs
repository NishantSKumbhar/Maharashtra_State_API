namespace PatanWalks.Models.DTO
{
    public class UpdateDistrictDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AreaInSqKm { get; set; }
        public string? DistrictImageUrl { get; set; }
        public Guid DivisionId { get; set; }
        public Guid PopulationId { get; set; }
    }
}
