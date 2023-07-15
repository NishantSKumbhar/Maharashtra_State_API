using System.ComponentModel.DataAnnotations;

namespace PatanWalks.Models.DTO
{
    public class DivisionPostDTO
    {
        [Required]
        [MinLength(4, ErrorMessage = "Code min length must be 4")]
        [MaxLength(4, ErrorMessage = "Code max length must be 4")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name max length must be 100")]
        public string Name { get; set; }
        public string? DivisionImageUrl { get; set; }
    }
}
