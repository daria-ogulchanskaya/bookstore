using System;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Data.Entities
{
    [Table("AspNetUsers")]
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsBlocked { get; set; }
    }
}
