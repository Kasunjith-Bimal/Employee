using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Infrastructure.Migrations
{
    public partial class AddInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "729db45a-8c49-44ec-b1e7-9f55d8b2fb20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcec6c82-41a8-4124-a7b4-fa656d59fc17");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12cadd49-b01a-411d-b747-34cc23ebd84c", "0b5bc486-8d8c-49a4-972a-198b29b690ce", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93b0a2a4-cfba-461c-b83f-671a0a063fe5", "db568bbb-e233-4666-b3ca-4a9e7532937c", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Salary", "SecurityStamp", "Telephone", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0223b443-2923-4006-afcb-96e0ec43c808", 0, "No 212,Walekade,Palugasdamana,Polonnaruwa.", "47ede88e-fa56-45eb-aad1-8b66960c60fc", "kasunysoft@gmail.com", false, "Mirahampe Patisthana Gedara Kasunjith Bimal Lakshitha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "KASUNYSOFT@GMAIL.COM", "KASUNYSOFT@GMAIL.COM", "AQAAAAEAACcQAAAAEGj0KpfqMsA/+3XNr5EMyx39bBn1j2QjxO6JqTUS/H7zPdY82RV8kCzLKtDhq8+8kg==", "0716063159", false, 333000m, "9b466131-9657-4453-9a7f-564269f257c1", "0716063159", false, "kasunysoft@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "12cadd49-b01a-411d-b747-34cc23ebd84c", "0223b443-2923-4006-afcb-96e0ec43c808" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93b0a2a4-cfba-461c-b83f-671a0a063fe5");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "12cadd49-b01a-411d-b747-34cc23ebd84c", "0223b443-2923-4006-afcb-96e0ec43c808" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12cadd49-b01a-411d-b747-34cc23ebd84c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0223b443-2923-4006-afcb-96e0ec43c808");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "729db45a-8c49-44ec-b1e7-9f55d8b2fb20", "9ac917e8-4a41-4e87-8a16-d8ea73865d75", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bcec6c82-41a8-4124-a7b4-fa656d59fc17", "f821bad7-13a0-48d3-9c2a-f465a8a95b99", "Employee", "EMPLOYEE" });
        }
    }
}
