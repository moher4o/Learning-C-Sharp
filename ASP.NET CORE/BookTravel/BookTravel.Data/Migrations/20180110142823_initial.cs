using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookTravel.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsOneWay = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalInformation = table.Column<string>(maxLength: 300, nullable: true),
                    ArrivalAirlineId = table.Column<int>(nullable: true),
                    ArrivalAirportId = table.Column<int>(nullable: true),
                    ArrivalDate = table.Column<DateTime>(nullable: true),
                    ArrivalFlightNumber = table.Column<string>(nullable: true),
                    ArrivalPassengers = table.Column<int>(nullable: false),
                    BabyAge = table.Column<int>(nullable: false),
                    BabyPassengers = table.Column<int>(nullable: false),
                    DepartureAirlineId = table.Column<int>(nullable: true),
                    DepartureAirportId = table.Column<int>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: true),
                    DepartureFlightNumber = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    HoldBags = table.Column<int>(nullable: false),
                    KlientName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    PickupLocation = table.Column<string>(nullable: true),
                    ReturnPassengers = table.Column<int>(nullable: false),
                    SkiBags = table.Column<int>(nullable: false),
                    SnowboardBags = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    TransferTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Airlines_ArrivalAirlineId",
                        column: x => x.ArrivalAirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Airports_ArrivalAirportId",
                        column: x => x.ArrivalAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Airlines_DepartureAirlineId",
                        column: x => x.DepartureAirlineId,
                        principalTable: "Airlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Airports_DepartureAirportId",
                        column: x => x.DepartureAirportId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_TransferTypes_TransferTypeId",
                        column: x => x.TransferTypeId,
                        principalTable: "TransferTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ArrivalAirlineId",
                table: "Transfers",
                column: "ArrivalAirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ArrivalAirportId",
                table: "Transfers",
                column: "ArrivalAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DepartureAirlineId",
                table: "Transfers",
                column: "DepartureAirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DepartureAirportId",
                table: "Transfers",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TransferTypeId",
                table: "Transfers",
                column: "TransferTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "TransferTypes");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
