using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Week6Lab_Identity.Migrations
{
    public partial class BetterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedLifetimeEarnings",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte>(
                name: "Location",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Location",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedLifetimeEarnings",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
