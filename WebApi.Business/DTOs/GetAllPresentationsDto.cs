namespace WebApi.Business.DTOs
{
    public class GetAllPresentationsDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Quantity { get; set; } = null!;
        public string UnitMeasure { get; set; } = null!;
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
    }
}
