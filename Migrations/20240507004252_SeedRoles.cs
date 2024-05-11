using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Identity;
using System;
using Movies.Authentication;

namespace Movies.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "Admin", "ADMIN" },
                    { "2", "User", "USER" }
                });

            var hasher = new PasswordHasher<AppUser>();
            var hashedPassword = hasher.HashPassword(null, "Admin12345");

            var securityStamp = Guid.NewGuid().ToString();
            var concurrencyStamp = Guid.NewGuid().ToString();

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "FirstName", "LastName", "YourFavirotePerson", "IsDeleted", "AccessFailedCount" , "PhoneNumberConfirmed" , "TwoFactorEnabled" , "LockoutEnabled" },
                values: new object[,]
                {
                    {
                        "1", "admin", "ADMIN", "admin@admin.com", "ADMIN@ADMIN.COM", true, hashedPassword, securityStamp, concurrencyStamp,
                        "Admin", "Admin", "Admin", false,0 , false, false, true
                    }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "1", "1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
