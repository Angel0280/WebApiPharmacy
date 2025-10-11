using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Entidades
{
    public class Purchases
    {
        public int PurcaseId {  get; set; }
        public Suppliers oSupplier { get; set; }
        public Users oUser { get; set; }
        public decimal Total { get; set; }
        public string Observations { get; set; }
        public string RegistedDate { get; set; }
        public string PurchaseNum { get; set; }
    }
}
