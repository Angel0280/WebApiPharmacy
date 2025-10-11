using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Entidades
{
    public class MasterReturn
    {
        public int MasterReturnId { get; set; }
        public string Reason { get; set; }
        public string ReturnPolicy { get; set; }
        public ProductReturns oReturn {  get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
        public decimal Total {get; set; }
    }
}
