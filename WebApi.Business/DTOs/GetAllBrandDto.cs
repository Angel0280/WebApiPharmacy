namespace WebApi.Business.DTOs
{
    public class GetAllBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandDescription { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }

        //Omitir la propiedad IsActive
    }
}
