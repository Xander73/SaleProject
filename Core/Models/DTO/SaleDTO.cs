using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Models.DTO
{
    public class SaleDTO
    {
        public Guid ProductId { get; set; }
        public IEnumerable<Dictionary<Guid, KeyValuePair<int, double>>> Products { get; set; }
            = new List<Dictionary<Guid, KeyValuePair<int, double>>>();
        public Guid BuyerId { get; set; }
    }
}
