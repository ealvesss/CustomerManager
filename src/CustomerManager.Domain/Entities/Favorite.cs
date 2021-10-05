using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManager.Domain.Entities
{
    public class Favorite : EntityBase
    {
        public Guid CustomerId { get; set; }
        public List<Product> Products { get; set; }

        public void OverwriteProducts(IEnumerable<Product> products)
        {
            // remove
            for (int i = 0; i < Products.Count; i++)
            {
                var oldProduct = Products[i];

                if (!products.Any(x => x.Id == oldProduct.Id))
                    Products.Remove(oldProduct);
            }

            // add new ones
            foreach (var product in products)
            {
                if (product.Id != Guid.Empty && Products.Any(x => x.Id == product.Id))
                    continue;

                Products.Add(product);
            }
        }
    }
}
