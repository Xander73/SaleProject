using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Models.Requests
{
    public class ProductRequest
    {
        public string ClientBaseAddres { get; set; }
        public Guid ProductId { get; set; }
    }
}
