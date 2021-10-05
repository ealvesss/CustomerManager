using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Application.Dtos
{
    public class ProductDto
    {
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Brand { get; set; }

        public string Title { get; set; }
    }
}
