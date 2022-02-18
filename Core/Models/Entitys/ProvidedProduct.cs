using System;

namespace Core.Models.Entitys
{
    public class ProvidedProduct
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set;}
        public int ProductQuantity { get; set;}
    }
}
