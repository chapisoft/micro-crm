﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartAdmin.Infrastructure.Persistence.Migrations
{
    public partial class fix_Contact_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contect",
                table: "Companies",
                newName: "Contact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Companies",
                newName: "Contect");
        }
    }
}
