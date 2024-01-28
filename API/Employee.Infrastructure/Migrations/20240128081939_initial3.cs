using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Infrastructure.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "AspNetUsers");

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
    }
}
