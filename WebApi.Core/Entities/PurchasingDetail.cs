using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Entities;

namespace WebApi.Core.Entidades
{
    public  class PurchasingDetail
    {
        public int PurchaseDetailId { get; set; }
        public Purchases oPurchase {  get; set; }
        public Products oProduct { get; set; }
        public ProductBatches oBatch { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }


    }
}
