using System;
using System.Collections.Generic;

namespace CustomerManager.Application.Dtos
{
    public class FavoriteRequestDto
    {
        public Guid FavoriteId { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<ProductRequestDto> Products { get; set; }
    }
}
