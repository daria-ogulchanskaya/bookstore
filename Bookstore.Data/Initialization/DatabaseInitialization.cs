using Bookstore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static Bookstore.Shared.Enums.Enums;

namespace Bookstore.Data.Initialization
{
    public class DatabaseInitialization
    {
        public static void Initialize(ModelBuilder builder)
        {
            builder.Entity<PrintingEdition>().HasData(PrintingEditionSeed());
            builder.Entity<Author>().HasData(AuthorSeed());
            builder.Entity<AuthorInPrintingEdition>().HasData(AuthorInPrintsSeed());
            builder.Entity<User>().HasData(UsersSeed());
            builder.Entity<Role>().HasData(RolesSeed());
            builder.Entity<IdentityUserRole<Guid>>().HasData(UserRolesSeed());
        }

        protected static List<User> UsersSeed() =>
            new List<User>
            {
                new User
                {
                    Id = new Guid("11D23E27-A4E6-47FC-A5FD-7038EE63B6A4"),
                    UserName = "admin",
                    Name = "Admin",
                    Surname = "Admin",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEN6GNXByKLxkYVSP90Tdxqfd4QEOa4BKOH03CDWZJb6WZXbUFXtDi2NL4O8qLhxW8g==",
                    SecurityStamp = "WYYGMLX7LH3YWIJNBCEK6HKE6TYOHNED"
                },

                new User
                {
                    Id = new Guid("b33b72eb-ab0d-47f1-b804-4ba2b2cc22d3"),
                    UserName = "sponge_bob",
                    Name = "Sponge Bob",
                    Surname = "Square Pants",
                    Email = "bob@gmail.com",
                    NormalizedEmail = "BOB@GMAIL.COM",
                    NormalizedUserName = "SPONGE_BOB",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAECpOtnJAR/Pc5Od4iN6SojXVU9YdFQCSkjfPif4A4T6nDgkLkywSAmUZ7eHWHv5oeQ==",
                    SecurityStamp = "4EA6CCR3M2J3WDLBHSH6SPLPCPHDP2CP"
                },
            };

        protected static List<IdentityUserRole<Guid>> UserRolesSeed() =>
            new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid> {RoleId = new Guid("8761E5F3-99EB-4F90-824F-33EBF007FB24"), UserId = new Guid("11D23E27-A4E6-47FC-A5FD-7038EE63B6A4")},
                new IdentityUserRole<Guid> {RoleId = new Guid("7c540037-fe09-4e53-899e-cfcfe960bc50"), UserId = new Guid("b33b72eb-ab0d-47f1-b804-4ba2b2cc22d3")}
            };

        protected static List<Role> RolesSeed() =>
            new List<Role>
            {
                new Role {Id = new Guid("8761E5F3-99EB-4F90-824F-33EBF007FB24"), Name = "Admin", NormalizedName = "ADMIN"},
                new Role {Id = new Guid("7c540037-fe09-4e53-899e-cfcfe960bc50"), Name = "Client", NormalizedName = "CLIENT"}
            };

        protected static List<PrintingEdition> PrintingEditionSeed() =>
            new List<PrintingEdition>
            {
                new PrintingEdition
                {
                    Id = new Guid("6974c065-a218-4416-b30d-7b087c13ba2a"),
                    Title = "Dandelion Wine",
                    Description = "Dandelion Wine is a 1957 novel by Ray Bradbury set in the summer of 1928 in the fictional town of Green Town, Illinois, based upon Bradbury's childhood home of Waukegan, Illinois.",
                    Price = 12,
                    Type = PrintingEditionType.Book,
                    Currency = CurrencyType.USD,
                    CreationDate = DateTime.UtcNow
                },

                new PrintingEdition
                {
                    Id = new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f"),
                    Title = "Pride and Prejudice",
                    Description = "Pride and Prejudice is a romantic novel of manners written by Jane Austen in 1813",
                    Price = 9,
                    Type = PrintingEditionType.Book,
                    Currency = CurrencyType.USD,
                    CreationDate = DateTime.UtcNow
                }
            };

        protected static List<Author> AuthorSeed() =>
            new List<Author>
            {
                new Author
                {
                    Id = new Guid("41c261be-dcd4-4953-964a-abbf6648343a"),
                    Name = "Ray Bradbury"
                },

                new Author
                {
                    Id = new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"),
                    Name = "Jane Austen"
                },

                new Author
                {
                    Id = new Guid("27520474-545c-4ead-b8a8-17c52e8c5c3f"),
                    Name = "Margaret Mitchell"
                }
            };

        protected static List<AuthorInPrintingEdition> AuthorInPrintsSeed() =>
            new List<AuthorInPrintingEdition>
            {
                new AuthorInPrintingEdition
                {
                    Id = new Guid("b39d55f1-066a-4c4f-9e64-cea59b41ac4f"),
                    AuthorId = new Guid("41c261be-dcd4-4953-964a-abbf6648343a"),
                    PrintingEditionId = new Guid("6974c065-a218-4416-b30d-7b087c13ba2a")
                },

                new AuthorInPrintingEdition
                {
                    Id = new Guid("8949a1a1-56d4-407e-98bc-02d9a4a922f3"),
                    AuthorId = new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"),
                    PrintingEditionId = new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f")
                }
            };
    }
}
