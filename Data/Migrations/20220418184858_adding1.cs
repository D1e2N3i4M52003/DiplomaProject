using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class adding1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
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

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_Title",
                table: "Posts",
                newName: "IX_Posts_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "City", "Description", "Name" },
                values: new object[] { new Guid("cf6f2ea9-6a78-4d0d-a7bf-bd00ef67e374"), "Plovdiv", "In Plovdiv", "Ancient Theathre" });

            migrationBuilder.InsertData(
                table: "Excursions",
                columns: new[] { "Id", "CreationDate", "EndsOnDate", "Name", "Price", "StartsOnDate" },
                values: new object[] { new Guid("57ba3b75-3e64-45f6-a58e-75220c139eff"), new DateTime(2022, 4, 18, 21, 48, 57, 690, DateTimeKind.Local).AddTicks(5556), new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "Plovdiv", 14m, new DateTime(2022, 4, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "Firstname", "Lastname", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("4ec90d27-6a1f-43b5-bbbf-c22f52d7e381"), new DateTime(2022, 4, 18, 21, 48, 56, 968, DateTimeKind.Local).AddTicks(6047), "admin@gmail.com", "Ad", "min", "$2a$11$RWyB6Jg/7aayKoY9.MipOeYYvg9ol7Ro3RtBe5aVFooFWvtlA7Frm", 0, "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("cf6f2ea9-6a78-4d0d-a7bf-bd00ef67e374"));

            migrationBuilder.DeleteData(
                table: "Excursions",
                keyColumn: "Id",
                keyValue: new Guid("57ba3b75-3e64-45f6-a58e-75220c139eff"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4ec90d27-6a1f-43b5-bbbf-c22f52d7e381"));

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Title",
                table: "Comments",
                newName: "IX_Comments_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
