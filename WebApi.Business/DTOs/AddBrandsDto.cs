using System.ComponentModel.DataAnnotations;

namespace WebApi.Business.DTOs
{
    public class AddBrandsDto
    {
        [Required]
        public required string BrandName { get; set; }
        public string? BrandDescription { get; set; }
    }
}
