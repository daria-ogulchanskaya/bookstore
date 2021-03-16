using System;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.Data.Entities
{
    [Table("AspNetRoles")]
    public class Role : IdentityRole<Guid>
    {
    }
}
