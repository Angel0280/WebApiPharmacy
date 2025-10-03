namespace WebApiPharmacy.DTOs
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

    public class  AddBrands
    {
        public string Name { get; set; }
        public string BrandDescription { get; set; }

    }
}
