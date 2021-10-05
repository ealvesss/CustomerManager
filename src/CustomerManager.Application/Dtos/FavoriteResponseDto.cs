using System;
using System.Collections.Generic;

namespace CustomerManager.Application.Dtos
{
    public class FavoriteResponseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<ProductRequestDto> Products { get; set; }
    }
}
