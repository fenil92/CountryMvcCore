using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVCAuth.Data.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Favorites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Country",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Country");
        }
    }
}
