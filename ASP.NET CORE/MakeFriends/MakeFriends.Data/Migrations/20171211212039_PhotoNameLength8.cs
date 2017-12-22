using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MakeFriends.Data.Migrations
{
    public partial class PhotoNameLength8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoName",
                table: "Images",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoName",
                table: "Images",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8);
        }
    }
}
