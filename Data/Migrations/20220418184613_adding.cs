using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class adding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("2d27d33f-9bfa-4f3a-ba0e-2429baa3556c"));

            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("57039ccf-6158-4907-b754-c341f6fb945f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6fc68de2-0839-4759-823f-c80a8f780fbe"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "City", "Description", "Name" },
                values: new object[] { new Guid("2d6f6f72-c8bb-4942-8b2e-237902851804"), "Plovdiv", "In Plovdiv", "Ancient Theathre" });

            migrationBuilder.InsertData(
                table: "Excursions",
                columns: new[] { "Id", "CreationDate", "EndsOnDate", "Name", "Price", "StartsOnDate" },
                values: new object[] { new Guid("39fb25e7-9526-44a4-8e0a-e49f646e8c2a"), new DateTime(2022, 4, 18, 21, 46, 12, 557, DateTimeKind.Local).AddTicks(6431), new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "Plovdiv", 14m, new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "Firstname", "Lastname", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("845998d5-e311-49b1-9f74-2f34130dc080"), new DateTime(2022, 4, 18, 21, 46, 12, 156, DateTimeKind.Local).AddTicks(2596), "admin@gmail.com", "Ad", "min", "$2a$11$tNHJ1Y8xGmVwMXcETGss4.LamCuMUsBKKlUtGFlkVmCxSML4zMExK", 0, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Title",
                table: "Comments",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_Title",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("2d6f6f72-c8bb-4942-8b2e-237902851804"));

            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("39fb25e7-9526-44a4-8e0a-e49f646e8c2a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("845998d5-e311-49b1-9f74-2f34130dc080"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "City", "Description", "Name" },
                values: new object[] { new Guid("2d27d33f-9bfa-4f3a-ba0e-2429baa3556c"), "Plovdiv", "In Plovdiv", "Ancient Theathre" });

            migrationBuilder.InsertData(
                table: "Excursions",
                columns: new[] { "Id", "CreationDate", "EndsOnDate", "Name", "Price", "StartsOnDate" },
                values: new object[] { new Guid("57039ccf-6158-4907-b754-c341f6fb945f"), new DateTime(2022, 4, 18, 19, 41, 5, 860, DateTimeKind.Local).AddTicks(3437), new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "Plovdiv", 14m, new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "Firstname", "Lastname", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("6fc68de2-0839-4759-823f-c80a8f780fbe"), new DateTime(2022, 4, 18, 19, 41, 5, 468, DateTimeKind.Local).AddTicks(1872), "admin@gmail.com", "Ad", "min", "$2a$11$Skn9fjNKwVOTEsJL4mYfgenfeRH58jv6okN3dGnbXxbH5NyejAxJC", 0, "Admin" });
        }
    }
}
