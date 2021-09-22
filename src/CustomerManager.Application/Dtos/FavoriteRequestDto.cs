using System;
using System.Collections.Generic;

namespace CustomerManager.Application.Dtos
{
    public class FavoriteRequestDto
    {
        public Guid CustomerId { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
