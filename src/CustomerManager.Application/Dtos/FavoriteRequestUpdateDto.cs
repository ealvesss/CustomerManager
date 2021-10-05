using System;
using System.Collections.Generic;

namespace CustomerManager.Application.Dtos
{
    public class FavoriteRequestUpdateDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<ProductRequestUpdateDto> Products {get;set;}
    }
}
