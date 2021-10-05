using System;

namespace CustomerManager.Application.Dtos
{
    public class ProductRequestUpdateDto
    {
        public Guid? Id { get; set; }
        public Guid ExternalId { get; set; }
    }
}
