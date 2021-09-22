using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManager.Domain.Entities
{
    public class Product : EntityBase
    {
        public Guid ExternalProductId { get; set; }
        
        [NotMapped]
        public decimal Price { get; set; }
        
        [NotMapped]
        public string Image { get; set; }
        
        [NotMapped]
        public string Brand { get; set; }
        
        [NotMapped]
        public string Title { get; set; }

    }
}
