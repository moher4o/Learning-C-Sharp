using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MakeFriends.Data.Migrations
{
    public partial class Color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EyesColour",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HairColour",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "EyesColor",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HairColor",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EyesColor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HairColor",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "EyesColour",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HairColour",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
