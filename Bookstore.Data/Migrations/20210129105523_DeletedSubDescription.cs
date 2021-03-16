﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Data.Migrations
{
    public partial class DeletedSubDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubDescription",
                table: "PrintingEditions");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c540037-fe09-4e53-899e-cfcfe960bc50"),
                column: "ConcurrencyStamp",
                value: "b0a81e4f-25ce-4561-9b22-093ade043806");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8761e5f3-99eb-4f90-824f-33ebf007fb24"),
                column: "ConcurrencyStamp",
                value: "c2ace9e1-ecab-41b3-b751-6312e8f2be31");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11d23e27-a4e6-47fc-a5fd-7038ee63b6a4"),
                column: "ConcurrencyStamp",
                value: "5efce842-83a0-496e-bab6-6a34a9e9e9b7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b33b72eb-ab0d-47f1-b804-4ba2b2cc22d3"),
                column: "ConcurrencyStamp",
                value: "9d923827-1d23-4e3c-8028-8078ea75856f");

            migrationBuilder.UpdateData(
                table: "AuthorInBooks",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { new Guid("41c261be-dcd4-4953-964a-abbf6648343a"), new Guid("6974c065-a218-4416-b30d-7b087c13ba2a") },
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 607, DateTimeKind.Utc).AddTicks(3582));

            migrationBuilder.UpdateData(
                table: "AuthorInBooks",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"), new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f") },
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 607, DateTimeKind.Utc).AddTicks(5546));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("27520474-545c-4ead-b8a8-17c52e8c5c3f"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 606, DateTimeKind.Utc).AddTicks(6513));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("41c261be-dcd4-4953-964a-abbf6648343a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 606, DateTimeKind.Utc).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 606, DateTimeKind.Utc).AddTicks(6441));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: new Guid("6974c065-a218-4416-b30d-7b087c13ba2a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 604, DateTimeKind.Utc).AddTicks(848));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 29, 10, 55, 22, 604, DateTimeKind.Utc).AddTicks(1151));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubDescription",
                table: "PrintingEditions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "empty");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7c540037-fe09-4e53-899e-cfcfe960bc50"),
                column: "ConcurrencyStamp",
                value: "79d44826-8870-4408-8fc1-000fca457a32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8761e5f3-99eb-4f90-824f-33ebf007fb24"),
                column: "ConcurrencyStamp",
                value: "c18cf4e4-a852-48f7-8248-ce3bc8b261e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("11d23e27-a4e6-47fc-a5fd-7038ee63b6a4"),
                column: "ConcurrencyStamp",
                value: "f7b605ae-88d4-4e18-9183-3bd30d9caf7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b33b72eb-ab0d-47f1-b804-4ba2b2cc22d3"),
                column: "ConcurrencyStamp",
                value: "b4a32549-57bd-434a-9e10-9914772f6d93");

            migrationBuilder.UpdateData(
                table: "AuthorInBooks",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { new Guid("41c261be-dcd4-4953-964a-abbf6648343a"), new Guid("6974c065-a218-4416-b30d-7b087c13ba2a") },
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 484, DateTimeKind.Utc).AddTicks(4837));

            migrationBuilder.UpdateData(
                table: "AuthorInBooks",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"), new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f") },
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 484, DateTimeKind.Utc).AddTicks(6465));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("27520474-545c-4ead-b8a8-17c52e8c5c3f"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 484, DateTimeKind.Utc).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("41c261be-dcd4-4953-964a-abbf6648343a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 483, DateTimeKind.Utc).AddTicks(8282));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("5aba11b4-64fc-4753-9a76-ab844ea05b2a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 484, DateTimeKind.Utc).AddTicks(160));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: new Guid("6974c065-a218-4416-b30d-7b087c13ba2a"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 481, DateTimeKind.Utc).AddTicks(279));

            migrationBuilder.UpdateData(
                table: "PrintingEditions",
                keyColumn: "Id",
                keyValue: new Guid("9e652fe1-bffd-4ef4-8648-ec5c6bdba09f"),
                column: "CreationDate",
                value: new DateTime(2021, 1, 28, 11, 30, 30, 481, DateTimeKind.Utc).AddTicks(519));
        }
    }
}
