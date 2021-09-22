using CustomerManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CustomerManager.Application.Dtos
{
    public class FavoriteResultDto
    {
        public Guid FavoriteId { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
