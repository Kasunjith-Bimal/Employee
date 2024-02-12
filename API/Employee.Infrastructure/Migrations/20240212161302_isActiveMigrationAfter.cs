using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Infrastructure.Migrations
{
    public partial class isActiveMigrationAfter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a72a4b7d-d9bd-4f26-bf85-ec86189f74ca", "9ba279f2-2cde-4c56-8517-48cdf1ddb8b3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf0df154-0cd4-4fcc-96bb-48a616334931", "9b5674ad-1aff-4764-ac49-3bf58d2911a4", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsActive", "IsFirstLogin", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Salary", "SecurityStamp", "Telephone", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2d5456a8-0030-442b-a77f-ea4bc399417b", 0, "No 212,Walekade,Palugasdamana,Polonnaruwa.", "68f1e090-27cb-44df-8d68-4f803aae2f1c", "kasunysoft@gmail.com", false, "Mirahampe Patisthana Gedara Kasunjith Bimal Lakshitha", true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "KASUNYSOFT@GMAIL.COM", "KASUNYSOFT@GMAIL.COM", "AQAAAAEAACcQAAAAEDtrSnFSyrr8eey0HWzWkploAeyqQav9yZJzljZbgen/AWmuPi3s9BOttbbIIilJqw==", "0716063159", false, 333000m, "b80c4325-abea-471b-92c1-9631c0d9d276", "0716063159", false, "kasunysoft@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a72a4b7d-d9bd-4f26-bf85-ec86189f74ca", "2d5456a8-0030-442b-a77f-ea4bc399417b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf0df154-0cd4-4fcc-96bb-48a616334931");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a72a4b7d-d9bd-4f26-bf85-ec86189f74ca", "2d5456a8-0030-442b-a77f-ea4bc399417b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a72a4b7d-d9bd-4f26-bf85-ec86189f74ca");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d5456a8-0030-442b-a77f-ea4bc399417b");

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
    }
}
