using System;
using System.Collections.Generic;

namespace Core.Models.Entitys
{
    [Serializable]
    public class Sale
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public Guid SalesPointId { get; set; }
        public Guid BuyerId { get; set; }
        public Buyer? Buyer { get; set; }
        public List<SaleData> SalesData { get; set; } = new List<SaleData>();    
        public double TotalAmount { get; set; }
    }
}
