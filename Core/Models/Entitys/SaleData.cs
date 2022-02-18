using System;

namespace Core.Models.Entitys
{
    public class SaleData
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductIdAmount { get; set; }
        public Guid SaleId { get; set; }
        public Sale? Sale { get; set; }
    }
}
