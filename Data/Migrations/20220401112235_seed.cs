using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationsExcursion_Excursions_ExcurrsionsId",
                table: "DestinationsExcursion");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcurrsionsId",
                table: "ExcursionUser");

            migrationBuilder.RenameColumn(
                name: "ExcurrsionsId",
                table: "ExcursionUser",
                newName: "ExcursionsId");

            migrationBuilder.RenameColumn(
                name: "ExcurrsionsId",
                table: "DestinationsExcursion",
                newName: "ExcursionsId");

            migrationBuilder.RenameIndex(
                name: "IX_DestinationsExcursion_ExcurrsionsId",
                table: "DestinationsExcursion",
                newName: "IX_DestinationsExcursion_ExcursionsId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreationDate", "Email", "Firstname", "Lastname", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("25bbc3cf-0f07-4d93-a5cc-6c3321859fa1"), new DateTime(2022, 4, 1, 14, 22, 34, 604, DateTimeKind.Local).AddTicks(3627), "admin@gmail.com", "Ad", "min", "$2a$11$BjqW.Mt0TAJyND9uCzIy1.bv/6CXgPn4zhDspwI8DuIEOoo4rQUle", 0, "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationsExcursion_Excursions_ExcursionsId",
                table: "DestinationsExcursion",
                column: "ExcursionsId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcursionsId",
                table: "ExcursionUser",
                column: "ExcursionsId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationsExcursion_Excursions_ExcursionsId",
                table: "DestinationsExcursion");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcursionsId",
                table: "ExcursionUser");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("25bbc3cf-0f07-4d93-a5cc-6c3321859fa1"));

            migrationBuilder.RenameColumn(
                name: "ExcursionsId",
                table: "ExcursionUser",
                newName: "ExcurrsionsId");

            migrationBuilder.RenameColumn(
                name: "ExcursionsId",
                table: "DestinationsExcursion",
                newName: "ExcurrsionsId");

            migrationBuilder.RenameIndex(
                name: "IX_DestinationsExcursion_ExcursionsId",
                table: "DestinationsExcursion",
                newName: "IX_DestinationsExcursion_ExcurrsionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationsExcursion_Excursions_ExcurrsionsId",
                table: "DestinationsExcursion",
                column: "ExcurrsionsId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcursionUser_Excursions_ExcurrsionsId",
                table: "ExcursionUser",
                column: "ExcurrsionsId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
