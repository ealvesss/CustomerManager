using System;

namespace CustomerManager.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
