using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaApi.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Runtime", "Title" },
                values: new object[,]
                {
                    { 1L, 60, "a" },
                    { 2L, 120, "b" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Number", "Seets" },
                values: new object[,]
                {
                    { 1L, 1, 10 },
                    { 2L, 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "Date", "FilmId", "RoomId" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 2, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1L });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "Date", "FilmId", "RoomId" },
                values: new object[] { 2L, new DateTime(1, 1, 1, 4, 0, 0, 0, DateTimeKind.Unspecified), 2L, 2L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
