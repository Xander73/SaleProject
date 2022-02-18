using _Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Models.Requests
{
    public class SaleRequest
    {
        public string ClientBaseAddres { get; set; }
        public BuyDTO Buy { get; set; }
    }
}
