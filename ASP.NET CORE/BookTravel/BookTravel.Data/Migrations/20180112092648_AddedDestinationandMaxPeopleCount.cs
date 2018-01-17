using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookTravel.Data.Migrations
{
    public partial class AddedDestinationandMaxPeopleCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "MaxPeopleCount",
                table: "TransferTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DestinationId",
                table: "Transfers",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Destinations_DestinationId",
                table: "Transfers",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Destinations_DestinationId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_DestinationId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "MaxPeopleCount",
                table: "TransferTypes");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Transfers");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Transfers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
