using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Entities;

namespace WebApi.Core.Entidades
{
    public class InventoryLoss
    {
        public int LowId { get; set; }
        public ProductBatches oBatch { get; set; }
        public int Quantity { get; set; }
        public Products oProduct { get; set; }
        public Users oUser { get; set; }

    }
}
