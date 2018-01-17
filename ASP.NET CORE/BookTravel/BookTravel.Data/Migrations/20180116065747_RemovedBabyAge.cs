using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookTravel.Data.Migrations
{
    public partial class RemovedBabyAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BabyAge",
                table: "Transfers");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Transfers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "BabyAge",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
