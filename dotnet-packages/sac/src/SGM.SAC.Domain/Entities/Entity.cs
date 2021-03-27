using System;

namespace SGM.SAC.Domain.Entities
{
    public  class Entity 
    {
        public Guid Id { get; private set; }

        public DateTime LastUpdate { get; set; }
        public DateTime InsertedAt { get; set; }


        protected Entity()
        {
            Id = Guid.NewGuid();
            InsertedAt = DateTime.UtcNow;
        }
    }
}