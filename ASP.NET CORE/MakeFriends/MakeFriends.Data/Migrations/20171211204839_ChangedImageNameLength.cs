using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MakeFriends.Data.Migrations
{
    public partial class ChangedImageNameLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoName",
                table: "Images",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhotoName",
                table: "Images",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4);
        }
    }
}
