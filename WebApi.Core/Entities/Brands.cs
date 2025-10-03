using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Entities
{
    public class Brands
    {
        public int Id { get; set; }
        public required string BrandName { get; set; }
        public string? Description { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
    }
}
