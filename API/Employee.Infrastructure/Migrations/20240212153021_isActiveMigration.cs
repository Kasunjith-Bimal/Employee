using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Infrastructure.Migrations
{
    public partial class isActiveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92a91703-c67c-4247-b563-8be8c2ef0c79");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0ac8fa22-e66f-4040-89e0-cd247e8844bd", "cec9dde3-f6bd-4b42-a368-306f428a13bb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ac8fa22-e66f-4040-89e0-cd247e8844bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cec9dde3-f6bd-4b42-a368-306f428a13bb");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "33be3b68-f4f0-4793-b110-4d7ae05ebf7b", "6be42897-12ca-4fd7-bcdf-6d8cdb195799", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3dc1eb08-8176-4029-afac-39f965fd0cd0", "3a4cd997-6d5a-42eb-abdc-2540c0469d7b", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsActive", "IsFirstLogin", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Salary", "SecurityStamp", "Telephone", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fb350b07-5105-4606-994e-1c9f7938d3a6", 0, "No 212,Walekade,Palugasdamana,Polonnaruwa.", "27186d86-d5a0-42b9-94ce-855ebac0b09e", "kasunysoft@gmail.com", false, "Mirahampe Patisthana Gedara Kasunjith Bimal Lakshitha", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "KASUNYSOFT@GMAIL.COM", "KASUNYSOFT@GMAIL.COM", "AQAAAAEAACcQAAAAEDfvb6PRsZCAsY0PHlwMeYME0dJhH617ZNec1t/eTanfOz8wYPb6zuT0EIA+kO7drw==", "0716063159", false, 333000m, "e13c642c-8da8-4fdf-a2fa-0cbeeeff5946", "0716063159", false, "kasunysoft@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3dc1eb08-8176-4029-afac-39f965fd0cd0", "fb350b07-5105-4606-994e-1c9f7938d3a6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33be3b68-f4f0-4793-b110-4d7ae05ebf7b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3dc1eb08-8176-4029-afac-39f965fd0cd0", "fb350b07-5105-4606-994e-1c9f7938d3a6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dc1eb08-8176-4029-afac-39f965fd0cd0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb350b07-5105-4606-994e-1c9f7938d3a6");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ac8fa22-e66f-4040-89e0-cd247e8844bd", "7ecc74dd-04d6-4ea3-bcab-57d4d67f6cf3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92a91703-c67c-4247-b563-8be8c2ef0c79", "a72dae99-5c7a-4ec1-92a3-bc92277ab4cc", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsFirstLogin", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Salary", "SecurityStamp", "Telephone", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cec9dde3-f6bd-4b42-a368-306f428a13bb", 0, "No 212,Walekade,Palugasdamana,Polonnaruwa.", "f8c402d6-dfc4-4dac-9bfe-a58032d757d0", "kasunysoft@gmail.com", false, "Mirahampe Patisthana Gedara Kasunjith Bimal Lakshitha", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "KASUNYSOFT@GMAIL.COM", "KASUNYSOFT@GMAIL.COM", "AQAAAAEAACcQAAAAEN1L6CmUMn+FTgYAZwKW38gJapCg3V6AYM5ZE9QpRleINpQFSpMDwRbDF32IYdELMQ==", "0716063159", false, 333000m, "832e13ca-1b34-4e3e-871c-3d0f7c5c71cc", "0716063159", false, "kasunysoft@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0ac8fa22-e66f-4040-89e0-cd247e8844bd", "cec9dde3-f6bd-4b42-a368-306f428a13bb" });
        }
    }
}
