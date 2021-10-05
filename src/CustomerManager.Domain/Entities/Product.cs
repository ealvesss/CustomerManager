using System;

namespace CustomerManager.Domain.Entities
{
    public class Product : EntityBase
    {
        public Guid ExternalProductId { get; set; }
        public Favorite Favorite {get;set;}

        public Guid FavoriteId { get; set; }
    }
}
