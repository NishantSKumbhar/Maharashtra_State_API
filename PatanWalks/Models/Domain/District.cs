namespace PatanWalks.Models.Domain
{
    public class District
    {
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
