using System;

namespace Bookstore.Data.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRemoved { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
