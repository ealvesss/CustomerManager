using System;

namespace CustomerManager.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
