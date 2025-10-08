using System.ComponentModel.DataAnnotations;

namespace WebApiPharmacy.DTOs
{
    public class AddBrandsDto
    {
        [Required]
        public required string BrandName { get; set; }
        public string? BrandDescription { get; set; }
    }
}
