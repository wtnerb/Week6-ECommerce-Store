using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Week6Lab_Identity.Migrations.Store
{
    public partial class FixBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserKey",
                table: "Basket",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserKey",
                table: "Basket",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
