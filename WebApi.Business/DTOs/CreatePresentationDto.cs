using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Business.DTOs
{
    public class CreatePresentationDto
    {
        [StringLength(100, ErrorMessage = "La descripcion no debe de exceder los 100 caracteres")]
        public string? Description { get; set; }
        [Required]
        [StringLength(50,ErrorMessage = "La cantidad de medida no debe de exceder los 50 caracteres")]
        public string Quantity { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "La unidad de medida no debe de exceder los 20 caracteres")]
        public string? UnitMeasure { get; set; }
    }
}
