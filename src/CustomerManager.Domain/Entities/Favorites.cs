using System.Collections.Generic;

namespace CustomerManager.Domain.Entities
{
    public class Favorites : EntityBase
    {
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
