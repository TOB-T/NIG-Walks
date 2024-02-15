using System.ComponentModel.DataAnnotations;

namespace NIGWalks.API.Models.DTO
{
    public class UpdateRegionRequestsDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a Maximum of 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of a 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
