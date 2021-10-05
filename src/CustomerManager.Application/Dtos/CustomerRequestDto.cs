using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager.Application.Dtos
{
   public  class CustomerRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
