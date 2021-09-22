using System;
using System.Collections.Generic;

namespace CustomerManager.Domain.Entities
{
    public class Favorite : EntityBase
    {
        public Guid CustomerId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
