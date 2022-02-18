using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Entitys
{
    public class Buyer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> SalesIds { get; set; } = new List<Guid>();
    }
}
