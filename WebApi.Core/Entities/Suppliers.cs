using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Entidades
{
    public class Suppliers
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string RNC { get; set; }
        public string Mail { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
