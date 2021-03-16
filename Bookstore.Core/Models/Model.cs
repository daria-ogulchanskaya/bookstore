using System;
using System.Collections.Generic;

namespace Bookstore.Core.Models
{
    public class Model
    {
        public Guid Id { get; set; }

        public ICollection<string> Errors;

        public DateTime CreationDate { get; set; }

        public Model()
        {
            Errors = new List<string>();
            CreationDate = DateTime.UtcNow;
        }

        public Model(Guid id)
        {
            Id = id;

            Errors = new List<string>();
            CreationDate = DateTime.UtcNow;
        }
    }
}
