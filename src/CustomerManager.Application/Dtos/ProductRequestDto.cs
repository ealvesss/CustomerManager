using System;

namespace CustomerManager.Application.Dtos
{
    public class ProductRequestDto
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
    }
}
