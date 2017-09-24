using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OneToMany.Migrations
{
    public partial class AddManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Employes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_ManagerId",
                table: "Employes",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_Employes_ManagerId",
                table: "Employes",
                column: "ManagerId",
                principalTable: "Employes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employes_Employes_ManagerId",
                table: "Employes");

            migrationBuilder.DropIndex(
                name: "IX_Employes_ManagerId",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employes");
        }
    }
}
