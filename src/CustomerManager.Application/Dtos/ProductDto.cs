using System;

namespace CustomerManager.Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
    }
}
