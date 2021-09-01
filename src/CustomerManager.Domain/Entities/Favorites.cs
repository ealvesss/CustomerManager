using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Domain.Entities
{
    public class Favorites : EntityBase
    {
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
