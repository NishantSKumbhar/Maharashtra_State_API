namespace PatanWalks.Models.DTO
{
    public class UpdateDistrictDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name max length must be 100")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description max length must be 1000")]
        public string Description { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Range should be 0, 100")]
        public double AreaInSqKm { get; set; }

        public string? DistrictImageUrl { get; set; }

        [Required]
        public Guid DivisionId { get; set; }

        [Required]
        public Guid PopulationId { get; set; }
    }
}
