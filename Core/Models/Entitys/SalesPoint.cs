using System;
using System.Collections.Generic;

namespace Core.Models.Entitys
{
    public class SalesPoint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ProvidedProduct> ProvidedProducts { get; set; } = new List<ProvidedProduct>();
    }
}
