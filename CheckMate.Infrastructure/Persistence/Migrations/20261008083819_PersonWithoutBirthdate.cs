using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckMate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersonWithoutBirthdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Persons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Persons",
                type: "date",
                nullable: true);
        }
    }
}
