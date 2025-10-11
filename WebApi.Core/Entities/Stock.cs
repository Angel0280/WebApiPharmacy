using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Core.Entidades
{
    public class Stock
    {

        //WILLIAM ES MANCO JAJA 

        public int StocId { get; set; }
        public ProductBatches oBatch { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
