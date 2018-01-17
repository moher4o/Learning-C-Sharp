using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookTravel.Data.Migrations
{
    public partial class DestinationCanBeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Transfers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Transfers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
