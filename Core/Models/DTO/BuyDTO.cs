using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Core.Models.DTO
{
    [Serializable]
    public class BuyDTO
    {
        public Guid BuyerId { get; set; }
        public IEnumerable<KeyValuePair<Guid, int>> ProductsIdQuantity { get; set; } = new List<KeyValuePair<Guid, int>>();
    }
}
