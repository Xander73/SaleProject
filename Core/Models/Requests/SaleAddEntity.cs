using Core.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Models.Requests
{
    public class SaleAddEntity
    {
        public string ClientBaseAddres { get; set; }
        public Sale SaleEntity { get; set; }
    }
}
